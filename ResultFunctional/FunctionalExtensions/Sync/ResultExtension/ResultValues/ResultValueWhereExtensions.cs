using System;
using System.Collections.Generic;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value functor function with conditions
    /// </summary>
    public static class ResultValueWhereExtensions
    {
        /// <summary>
        /// Execute result value function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultValueContinue<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, TValueOut> okFunc,
                                                                                       Func<TValueIn, IEnumerable<IErrorResult>> badFunc) =>
         @this.OkStatus 
             ? predicate(@this.Value)
                 ? new ResultValue<TValueOut>(okFunc.Invoke(@this.Value))
                 : new ResultValue<TValueOut>(badFunc.Invoke(@this.Value))
             : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute result value function base on predicate condition returning value in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultValueWhere<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, TValueOut> okFunc,
                                                                                       Func<TValueIn, TValueOut> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? new ResultValue<TValueOut>(okFunc.Invoke(@this.Value))
                 : new ResultValue<TValueOut>(badFunc.Invoke(@this.Value))
             : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute result value function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultValueOkBad<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, TValueOut> okFunc,
                                                                                    Func<IReadOnlyCollection<IErrorResult>, TValueOut> badFunc) =>
            @this.OkStatus
                ? new ResultValue<TValueOut>(okFunc.Invoke(@this.Value))
                : new ResultValue<TValueOut>(badFunc.Invoke(@this.Errors));

        /// <summary>
        /// Execute result value function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultValueOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this, 
                                                                                 Func<TValueIn, TValueOut> okFunc) =>
            @this.OkStatus
                ? new ResultValue<TValueOut>(okFunc.Invoke(@this.Value))
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute result value function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValue> ResultValueBad<TValue>(this IResultValue<TValue> @this,
                                                                  Func<IReadOnlyCollection<IErrorResult>, TValue> badFunc) =>

            @this.OkStatus
                ? @this
                : new ResultValue<TValue>(badFunc.Invoke(@this.Errors));

        /// <summary>
        /// Check errors by predicate to result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result value</returns>
        public static IResultValue<TValue> ResultValueCheckErrorsOk<TValue>(this IResultValue<TValue> @this,
                                                                           Func<TValue, bool> predicate,
                                                                           Func<TValue, IEnumerable<IErrorResult>> badFunc) =>
            @this.
            ResultValueContinue(predicate,
                                value => value,
                                badFunc);
    }
}