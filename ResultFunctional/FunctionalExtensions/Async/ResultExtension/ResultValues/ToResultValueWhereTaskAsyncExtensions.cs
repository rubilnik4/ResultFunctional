using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Task result value extension methods with condition
    /// </summary>
    public static class ToResultValueWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Converting task value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ToResultValueWhereTaskAsync<TValue>(this Task<TValue> @this,
                                                                                           Func<TValue, bool> predicate,
                                                                                           Func<TValue, IRError> badFunc)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToResultValueWhere(predicate, badFunc));

        /// <summary>
        /// Converting task value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ToResultValueWhereNullTaskAsync<TValue>(this Task<TValue?> @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, IRError> badFunc)
            where TValue : class =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToResultValueWhereNull(predicate, badFunc));

        /// <summary>
        /// Converting task value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ToResultValueWhereNullTaskAsync<TValue>(this Task<TValue?> @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, IRError> badFunc)
            where TValue : struct =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToResultValueWhereNull(predicate, badFunc));

        /// <summary>
        /// Converting task value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValueOut>> ToResultValueWhereNullOkBadTaskAsync<TValueIn, TValueOut>(this Task<TValueIn?> @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, TValueOut> okFunc,
                                                                                      Func<TValueIn?, IRError> badFunc)
            where TValueIn : class
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToResultValueWhereNullOkBad(predicate, okFunc, badFunc));

        /// <summary>
        /// Converting task value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValueOut>> ToResultValueWhereNullOkBadTaskAsync<TValueIn, TValueOut>(this Task<TValueIn?> @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, TValueOut> okFunc,
                                                                                      Func<TValueIn?, IRError> badFunc)
            where TValueIn : struct
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ToResultValueWhereNullOkBad(predicate, okFunc, badFunc));
    }
}