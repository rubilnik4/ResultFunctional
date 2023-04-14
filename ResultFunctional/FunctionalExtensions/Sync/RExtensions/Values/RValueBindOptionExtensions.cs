using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values
{
    /// <summary>
    /// Extension methods for result value monad function with conditions
    /// </summary>
    public static class RValueBindOptionExtensions
    {
        /// <summary>
        /// Execute monad result value function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> RValueBindOption<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                               Func<TValueIn, bool> predicate,
                                                                               Func<TValueIn, IRValue<TValueOut>> someFunc,
                                                                               Func<TValueIn, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? someFunc.Invoke(@this.GetValue())
                 :noneFunc.Invoke(@this.GetValue()).ToRValue<TValueOut>()
             : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute monad result value function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> RValueBindOption<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                               Func<TValueIn, bool> predicate,
                                                                               Func<TValueIn, IRValue<TValueOut>> someFunc,
                                                                               Func<TValueIn, IEnumerable<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.RValueBindOption(predicate, someFunc, value => noneFunc(value).ToList());

        /// <summary>
        /// Execute monad result value function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> RValueBindWhere<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                   Func<TValueIn, bool> predicate,
                                                                                   Func<TValueIn, IRValue<TValueOut>> someFunc,
                                                                                   Func<TValueIn, IRValue<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? someFunc.Invoke(@this.GetValue())
                 : noneFunc.Invoke(@this.GetValue())
             : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute monad result value function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <param name="noneFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> RValueBindMatch<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                        Func<TValueIn, IRValue<TValueOut>> someFunc,
                                                                                        Func<IReadOnlyCollection<IRError>, IRValue<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? someFunc.Invoke(@this.GetValue())
             : noneFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Execute monad result value function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> RValueBindSome<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                     Func<TValueIn, IRValue<TValueOut>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? someFunc.Invoke(@this.GetValue())
                : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute monad result value function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="noneFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> RValueBindNone<TValue>(this IRValue<TValue> @this,
                                                                      Func<IReadOnlyCollection<IRError>, IRValue<TValue>> noneFunc)
            where TValue : notnull =>
            @this.Success
                ? @this
                : noneFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Adding errors to result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Error function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> RValueBindEnsure<TValue>(this IRValue<TValue> @this, Func<TValue, IROption> someFunc)
            where TValue : notnull =>
            @this.
            RValueBindSome(value => someFunc.Invoke(value).ToRValue(value));
    }
}