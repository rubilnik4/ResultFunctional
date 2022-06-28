using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Async extension methods for actions
    /// </summary>
    public static class VoidAsyncExtensions
    {
        /// <summary>
        /// Execute async action
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged source</returns>     
        public static async Task<TValue> VoidAsync<TValue>(this TValue @this, Func<TValue, Task> action)
        {
            await action.Invoke(@this);
            return @this;
        }

        /// <summary>
        /// Execute async action with predicate
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged source</returns>
        public static async Task<TValue> VoidOkAsync<TValue>(this TValue @this, Func<TValue, bool> predicate,
                                                             Func<TValue, Task> action) =>
            predicate(@this)
                ? await @this.VoidAsync(_ => action.Invoke(@this))
                : @this;

        /// <summary>
        /// Execute async action base on predicate condition
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="actionOk">Action if predicate <see langword="true"/></param>
        /// <param name="actionBad">Action if predicate <see langword="false"/></param>
        /// <returns>Unchanged source</returns>
        public static async Task<TValue> VoidWhereAsync<TValue>(this TValue @this, 
                                                                Func<TValue, bool> predicate,
                                                                Func<TValue, Task> actionOk,
                                                                Func<TValue, Task> actionBad) =>
            predicate(@this)
                ? await @this.VoidAsync(_ => actionOk.Invoke(@this))
                : await @this.VoidAsync(_ => actionBad.Invoke(@this));
    }
}