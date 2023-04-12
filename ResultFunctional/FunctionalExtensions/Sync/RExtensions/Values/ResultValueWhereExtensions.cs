using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Values
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
        public static IRValue<TValueOut> ResultValueContinue<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                  Func<TValueIn, bool> predicate,
                                                                                  Func<TValueIn, TValueOut> okFunc,
                                                                                  Func<TValueIn, IReadOnlyCollection<IRError>> badFunc)
             where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? okFunc.Invoke(@this.GetValue()).ToRValue()
                 : badFunc.Invoke(@this.GetValue()).ToRValue<TValueOut>()
             : @this.GetErrors().ToRValue<TValueOut>();

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
        public static IRValue<TValueOut> ResultValueWhere<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, TValueOut> okFunc,
                                                                                       Func<TValueIn, TValueOut> badFunc)
             where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? okFunc.Invoke(@this.GetValue()).ToRValue()
                 : badFunc.Invoke(@this.GetValue()).ToRValue()
             : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute result value function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> ResultValueOkBad<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                    Func<TValueIn, TValueOut> okFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, TValueOut> badFunc)
             where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? okFunc.Invoke(@this.GetValue()).ToRValue()
                : badFunc.Invoke(@this.GetErrors()).ToRValue();

        /// <summary>
        /// Execute result value function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> ResultValueOk<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                 Func<TValueIn, TValueOut> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? okFunc.Invoke(@this.GetValue()).ToRValue()
                : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute result value function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ResultValueBad<TValue>(this IRValue<TValue> @this,
                                                                  Func<IReadOnlyCollection<IRError>, TValue> badFunc)
            where TValue : notnull =>

            @this.Success
                ? @this
                : badFunc.Invoke(@this.GetErrors()).ToRValue();

        /// <summary>
        /// Check errors by predicate to result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result value</returns>
        public static IRValue<TValue> ResultValueCheckErrorsOk<TValue>(this IRValue<TValue> @this,
                                                                           Func<TValue, bool> predicate,
                                                                           Func<TValue, IReadOnlyCollection<IRError>> badFunc)
            where TValue : notnull =>
            @this.
            ResultValueContinue(predicate,
                                value => value,
                                badFunc);
    }
}