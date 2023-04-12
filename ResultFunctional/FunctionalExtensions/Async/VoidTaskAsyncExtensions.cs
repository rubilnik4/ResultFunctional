using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Extension methods for task actions
    /// </summary>
    public static class VoidTaskAsyncExtensions
    {
        /// <summary>
        /// Execute task action
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged source</returns>   
        public static async Task<TValue> VoidTaskAsync<TValue>(this Task<TValue> @this, Action<TValue> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.Void(action));

        /// <summary>
        /// Execute task action with predicate
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged source</returns>
        public static async Task<TValue> VoidOkTaskAsync<TValue>(this Task<TValue> @this, Func<TValue, bool> predicate,
                                                                 Action<TValue> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.VoidSome(predicate, action));

        /// <summary>
        /// Execute task action base on predicate condition
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="actionSome">Action if predicate <see langword="true"/></param>
        /// <param name="actionNone">Action if predicate <see langword="false"/></param>
        /// <returns>Unchanged source</returns>
        public static async Task<TValue> VoidWhereTaskAsync<TValue>(this Task<TValue> @this, Func<TValue, bool> predicate,
                                                                    Action<TValue> actionSome, Action<TValue> actionNone) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.VoidMatch(predicate, actionSome, actionNone));
    }
}