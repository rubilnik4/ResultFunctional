using System;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
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
        public static IResultValue<TValue> ToResultValueWhere<TValue>(this TValue @this, Func<TValue, bool> predicate,
                                                                      Func<TValue, IRError> badFunc)
            where TValue : notnull =>
            @this.WhereContinue(predicate,
                                value => (IResultValue<TValue>)new ResultValue<TValue>(value),
                                value => new ResultValue<TValue>(badFunc(value)));

        /// <summary>
        /// Converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValue> ToResultValueWhereNull<TValue>(this TValue? @this, Func<TValue, bool> predicate,
                                                                          Func<TValue?, IRError> badFunc)
            where TValue : class =>
            @this.ToResultValueNullCheck(badFunc(@this)).
            ResultValueBindOk(value => value.ToResultValueWhere(predicate, badFunc));

        /// <summary>
        /// Converting value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValue> ToResultValueWhereNull<TValue>(this TValue? @this, Func<TValue, bool> predicate,
                                                                          Func<TValue?, IRError> badFunc)
            where TValue : struct =>
            @this.ToResultValueNullCheck(badFunc(@this)).
            ResultValueBindOk(value => value.ToResultValueWhere(predicate,
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
        public static IResultValue<TValueOut> ToResultValueWhereNullOkBad<TValueIn, TValueOut>(this TValueIn? @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, TValueOut> okFunc,
                                                                                      Func<TValueIn?, IRError> badFunc)
            where TValueIn : class
            where TValueOut : notnull =>
            @this.ToResultValueNullCheck(badFunc(@this)).
            ResultValueBindWhere(predicate,
                                 value => new ResultValue<TValueOut>(okFunc(value)),
                                 value => new ResultValue<TValueOut>(badFunc(value)));

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
        public static IResultValue<TValueOut> ToResultValueWhereNullOkBad<TValueIn, TValueOut>(this TValueIn? @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, TValueOut> okFunc,
                                                                                      Func<TValueIn?, IRError> badFunc)
            where TValueIn : struct
            where TValueOut : notnull =>
            @this.ToResultValueNullCheck(badFunc(@this)).
            ResultValueBindWhere(predicate,
                                 value => new ResultValue<TValueOut>(okFunc(value)),
                                 value => new ResultValue<TValueOut>(badFunc(value)));
    }
}