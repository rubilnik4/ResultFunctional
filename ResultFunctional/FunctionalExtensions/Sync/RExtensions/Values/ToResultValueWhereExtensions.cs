using System;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Values
{
    /// <summary>
    /// Result value extension methods with condition
    /// </summary>
    public static class ToResultValueWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ToRValueWhere<TValue>(this TValue @this, Func<TValue, bool> predicate,
                                                                 Func<TValue, IRError> badFunc)
            where TValue : notnull =>
            @this.WhereContinue(predicate,
                                value => value.ToRValue(),
                                value => badFunc(value).ToRValue<TValue>());

        /// <summary>
        /// Converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ToRValueWhereNull<TValue>(this TValue? @this, Func<TValue, bool> predicate,
                                                                          Func<TValue?, IRError> badFunc)
            where TValue : class =>
            @this.ToRValueNullCheck(badFunc(@this)).
            ResultValueBindOk(value => value.ToRValueWhere(predicate, badFunc));

        /// <summary>
        /// Converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ToRValueWhereNull<TValue>(this TValue? @this, Func<TValue, bool> predicate,
                                                                          Func<TValue?, IRError> badFunc)
            where TValue : struct =>
            @this.ToRValueNullCheck(badFunc(@this)).
            ResultValueBindOk(value => value.ToRValueWhere(predicate,
                                                                valueWhere => badFunc(valueWhere)));

        /// <summary>
        /// Converting value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> ToRValueWhereNullOkBad<TValueIn, TValueOut>(this TValueIn? @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, TValueOut> okFunc,
                                                                                      Func<TValueIn?, IRError> badFunc)
            where TValueIn : class
            where TValueOut : notnull =>
            @this.ToRValueNullCheck(badFunc(@this)).
            ResultValueBindWhere(predicate,
                                 value => okFunc(value).ToRValue(),
                                 value => badFunc(value).ToRValue<TValueOut>());

        /// <summary>
        /// Converting value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValueOut> ToRValueWhereNullOkBad<TValueIn, TValueOut>(this TValueIn? @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, TValueOut> okFunc,
                                                                                      Func<TValueIn?, IRError> badFunc)
            where TValueIn : struct
            where TValueOut : notnull =>
            @this.ToRValueNullCheck(badFunc(@this)).
            ResultValueBindWhere(predicate,
                                 value => okFunc(value).ToRValue(),
                                 value => badFunc(value).ToRValue<TValueOut>());
    }
}