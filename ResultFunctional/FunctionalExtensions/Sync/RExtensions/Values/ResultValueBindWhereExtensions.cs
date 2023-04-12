using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Values
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
        public static IRValue<TValueOut> ResultValueBindContinue<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, IRValue<TValueOut>> okFunc,
                                                                                      Func<TValueIn, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? okFunc.Invoke(@this.GetValue())
                 :badFunc.Invoke(@this.GetValue()).ToRValue<TValueOut>()
             : @this.GetErrors().ToRValue<TValueOut>();

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
        public static IRValue<TValueOut> ResultValueBindWhere<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, IRValue<TValueOut>> okFunc,
                                                                                       Func<TValueIn, IRValue<TValueOut>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? okFunc.Invoke(@this.GetValue())
                 : badFunc.Invoke(@this.GetValue())
             : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute monad result value function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> ResultValueBindOkBad<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                        Func<TValueIn, IRValue<TValueOut>> okFunc,
                                                                                        Func<IReadOnlyCollection<IRError>, IRValue<TValueOut>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? okFunc.Invoke(@this.GetValue())
             : badFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Execute monad result value function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> ResultValueBindOk<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                     Func<TValueIn, IRValue<TValueOut>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? okFunc.Invoke(@this.GetValue())
                : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute monad result value function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="badFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ResultValueBindBad<TValue>(this IRValue<TValue> @this,
                                                                      Func<IReadOnlyCollection<IRError>, IRValue<TValue>> badFunc)
            where TValue : notnull =>
            @this.Success
                ? @this
                : badFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Adding errors to result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Error function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ResultValueBindErrorsOk<TValue>(this IRValue<TValue> @this,
                                                                      Func<TValue, IROption> okFunc)
            where TValue : notnull =>
            @this.
            ResultValueBindOk(value => okFunc.Invoke(value).ToRValue(value));
    }
}