using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values
{
    /// <summary>
    /// Extension methods for result value functor function with conditions
    /// </summary>
    public static class RValueOptionExtensions
    {
        /// <summary>
        /// Execute result value function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> RValueOption<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                           Func<TValueIn, bool> predicate,
                                                                           Func<TValueIn, TValueOut> someFunc,
                                                                           Func<TValueIn, IEnumerable<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? someFunc.Invoke(@this.GetValue()).ToRValue()
                    : noneFunc.Invoke(@this.GetValue()).ToRValue<TValueOut>()
                : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute result value function base on predicate condition returning value in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> RValueWhere<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                          Func<TValueIn, bool> predicate,
                                                                          Func<TValueIn, TValueOut> someFunc,
                                                                          Func<TValueIn, TValueOut> noneFunc)
             where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? someFunc.Invoke(@this.GetValue()).ToRValue()
                 : noneFunc.Invoke(@this.GetValue()).ToRValue()
             : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute result value function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <param name="noneFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> RValueMatch<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                          Func<TValueIn, TValueOut> someFunc,
                                                                          Func<IReadOnlyCollection<IRError>, TValueOut> noneFunc)
             where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? someFunc.Invoke(@this.GetValue()).ToRValue()
                : noneFunc.Invoke(@this.GetErrors()).ToRValue();

        /// <summary>
        /// Execute result value function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> RValueSome<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                         Func<TValueIn, TValueOut> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? someFunc.Invoke(@this.GetValue()).ToRValue()
                : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute result value function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="noneFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> RValueNone<TValue>(this IRValue<TValue> @this,
                                                         Func<IReadOnlyCollection<IRError>, TValue> noneFunc)
            where TValue : notnull =>

            @this.Success
                ? @this
                : noneFunc.Invoke(@this.GetErrors()).ToRValue();

        /// <summary>
        /// Check errors by predicate to result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result value</returns>
        public static IRValue<TValue> RValueEnsure<TValue>(this IRValue<TValue> @this, Func<TValue, bool> predicate,
                                                           Func<TValue, IEnumerable<IRError>> noneFunc)
            where TValue : notnull =>
            @this.RValueOption(predicate, value => value, noneFunc);
    }
}