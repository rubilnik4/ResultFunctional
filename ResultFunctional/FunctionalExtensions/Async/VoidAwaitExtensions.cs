﻿using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Extension methods for task actions
    /// </summary>
    public static class VoidAwaitExtensions
    {
        /// <summary>
        /// Execute async task action
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged source</returns>   
        public static async Task<TValue> VoidAwait<TValue>(this Task<TValue> @this, Func<TValue, Task> action) =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.VoidAsync(action));

        /// <summary>
        /// Execute async task action with predicate
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged source</returns>
        public static async Task<TValue> VoidSomeAwait<TValue>(this Task<TValue> @this, Func<TValue, bool> predicate,
                                                                 Func<TValue, Task> action) =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.VoidSomeAsync(predicate, action));

        /// <summary>
        /// Execute async task action base on predicate condition
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="actionSome">Action if predicate <see langword="true"/></param>
        /// <param name="actionNone">Action if predicate <see langword="false"/></param>
        /// <returns>Unchanged source</returns>
        public static async Task<TValue> VoidOptionAwait<TValue>(this Task<TValue> @this, Func<TValue, bool> predicate,
                                                                 Func<TValue, Task> actionSome, Func<TValue, Task> actionNone) =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.VoidOptionAsync(predicate, actionSome, actionNone));
    }
}