using System;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values
{
    /// <summary>
    /// Result value extension methods with condition
    /// </summary>
    public static class ToRValueOptionExtensions
    {
        /// <summary>
        /// Converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ToRValueOption<TValue>(this TValue @this, Func<TValue, bool> predicate,
                                                                 Func<TValue, IRError> noneFunc)
            where TValue : notnull =>
            @this.Option(predicate,
                                value => value.ToRValue(),
                                value => noneFunc(value).ToRValue<TValue>());

        /// <summary>
        /// Converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ToRValueEnsureOption<TValue>(this TValue? @this, Func<TValue, bool> predicate,
                                                                          Func<TValue?, IRError> noneFunc)
            where TValue : class =>
            @this.ToRValueNullEnsure(noneFunc(@this)).
            RValueBindSome(value => value.ToRValueOption(predicate, noneFunc));

        /// <summary>
        /// Converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ToRValueEnsureOption<TValue>(this TValue? @this, Func<TValue, bool> predicate,
                                                                          Func<TValue?, IRError> noneFunc)
            where TValue : struct =>
            @this.ToRValueNullEnsure(noneFunc(@this)).
            RValueBindSome(value => value.ToRValueOption(predicate,
                                                                valueWhere => noneFunc(valueWhere)));

        /// <summary>
        /// Converting value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> ToRValueEnsureWhere<TValueIn, TValueOut>(this TValueIn? @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, TValueOut> someFunc,
                                                                                      Func<TValueIn?, IRError> noneFunc)
            where TValueIn : class
            where TValueOut : notnull =>
            @this.ToRValueNullEnsure(noneFunc(@this)).
            RValueBindWhere(predicate,
                                 value => someFunc(value).ToRValue(),
                                 value => noneFunc(value).ToRValue<TValueOut>());

        /// <summary>
        /// Converting value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> ToRValueEnsureWhere<TValueIn, TValueOut>(this TValueIn? @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, TValueOut> someFunc,
                                                                                      Func<TValueIn?, IRError> noneFunc)
            where TValueIn : struct
            where TValueOut : notnull =>
            @this.ToRValueNullEnsure(noneFunc(@this)).
            RValueBindWhere(predicate,
                                 value => someFunc(value).ToRValue(),
                                 value => noneFunc(value).ToRValue<TValueOut>());
    }
}