using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Extension methods for task actions
    /// </summary>
    public static class VoidBindAsyncExtensions
    {
        /// <summary>
        /// Execute async task action
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged source</returns>   
        public static async Task<TValue> VoidBindAsync<TValue>(this Task<TValue> @this, Func<TValue, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.VoidAsync(action));

        /// <summary>
        /// Execute async task action with predicate
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged source</returns>
        public static async Task<TValue> VoidOkBindAsync<TValue>(this Task<TValue> @this, Func<TValue, bool> predicate,
                                                                 Func<TValue, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.VoidOkAsync(predicate, action));

        /// <summary>
        /// Execute async task action base on predicate condition
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="actionSome">Action if predicate <see langword="true"/></param>
        /// <param name="actionNone">Action if predicate <see langword="false"/></param>
        /// <returns>Unchanged source</returns>
        public static async Task<TValue> VoidWhereBindAsync<TValue>(this Task<TValue> @this, Func<TValue, bool> predicate,
                                                                 Func<TValue, Task> actionSome, Func<TValue, Task> actionNone) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.VoidWhereAsync(predicate, actionSome, actionNone));
    }
}