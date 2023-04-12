using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Task result value async extension methods with condition
    /// </summary>
    public static class ToResultValueWhereBindAsyncExtensions
    {
        /// <summary>
        /// Async converting task value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueWhereBindAsync<TValue>(this Task<TValue> @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue, Task<IRError>> badFunc)
            where TValue : notnull =>
          await @this.
          MapBindAsync(thisAwaited => thisAwaited.ToResultValueWhereAsync(predicate, badFunc));

        /// <summary>
        /// Async converting task value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueWhereNullBindAsync<TValue>(this Task<TValue?> @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, Task<IRError>> badFunc)
            where TValue : class =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueWhereNullAsync(predicate, badFunc));

        /// <summary>
        /// Async converting task value to result value base on predicate
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueWhereNullBindAsync<TValue>(this Task<TValue?> @this,
                                                                                      Func<TValue, bool> predicate,
                                                                                      Func<TValue?, Task<IRError>> badFunc)
            where TValue : struct =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueWhereNullAsync(predicate, badFunc));

        /// <summary>
        /// Async converting task value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> ToResultValueWhereNullOkBadBindAsync<TValueIn, TValueOut>(this Task<TValueIn?> @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                      Func<TValueIn?, Task<IRError>> badFunc)
            where TValueIn : class
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueWhereNullOkBadAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Async converting task value to result value base on predicate base with functor function
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Functor function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Error function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> ToResultValueWhereNullOkBadBindAsync<TValueIn, TValueOut>(this Task<TValueIn?> @this,
                                                                                      Func<TValueIn, bool> predicate,
                                                                                      Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                      Func<TValueIn?, Task<IRError>> badFunc)
            where TValueIn : struct
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueWhereNullOkBadAsync(predicate, okFunc, badFunc));
    }
}