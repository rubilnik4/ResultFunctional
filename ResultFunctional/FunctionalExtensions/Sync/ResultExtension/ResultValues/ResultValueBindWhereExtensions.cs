using System;
using System.Collections.Generic;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value monad function with conditions
    /// </summary>
    public static class ResultValueBindWhereExtensions
    {
        /// <summary>
        /// Execute monad result value function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultValueBindContinue<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, IResultValue<TValueOut>> okFunc,
                                                                                       Func<TValueIn, IEnumerable<IRError>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? okFunc.Invoke(@this.Value)
                 : new ResultValue<TValueOut>(badFunc.Invoke(@this.Value))
             : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute monad result value function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultValueBindWhere<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, IResultValue<TValueOut>> okFunc,
                                                                                       Func<TValueIn, IResultValue<TValueOut>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? okFunc.Invoke(@this.Value)
                 : badFunc.Invoke(@this.Value)
             : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute monad result value function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultValueBindOkBad<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                        Func<TValueIn, IResultValue<TValueOut>> okFunc,
                                                                                        Func<IReadOnlyCollection<IRError>, IResultValue<TValueOut>> badFunc) =>
         @this.OkStatus
             ? okFunc.Invoke(@this.Value)
             : badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Execute monad result value function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultValueBindOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                     Func<TValueIn, IResultValue<TValueOut>> okFunc) =>
            @this.OkStatus
                ? okFunc.Invoke(@this.Value)
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute monad result value function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="badFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValue> ResultValueBindBad<TValue>(this IResultValue<TValue> @this,
                                                                      Func<IReadOnlyCollection<IRError>, IResultValue<TValue>> badFunc) =>
            @this.OkStatus
                ? @this
                : badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Adding errors to result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Error function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValue> ResultValueBindErrorsOk<TValue>(this IResultValue<TValue> @this,
                                                                           Func<TValue, IResultError> okFunc) =>
            @this.
            ResultValueBindOk(value => okFunc.Invoke(value).
                                       Map(resultError => resultError.ToResultValue(value)));
    }
}