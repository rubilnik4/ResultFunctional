using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Task result collection async action extension methods
    /// </summary>
    public static class RListVoidAwaitExtensions
    {
        /// <summary>
        /// Execute async action if task result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>    
        public static async Task<IRList<TValue>> RListVoidSomeAwait<TValue>(this Task<IRList<TValue>> @this,
                                                                                         Func<IReadOnlyCollection<TValue>, Task> action)
            where TValue : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.RListVoidSomeAsync(action));

        /// <summary>
        /// Execute async action if task result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result collection</returns>
        public static async Task<IRList<TValue>> RListVoidNoneAwait<TValue>(this Task<IRList<TValue>> @this,
                                                                                      Func<IReadOnlyCollection<IRError>, Task> action)
            where TValue : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.RListVoidNoneAsync(action));

        /// <summary>
        /// Execute async action depending on task result collection errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="actionSome">Action if result collection hasn't errors</param>
        /// <param name="actionNone">Action if result collection has errors</param>
        /// <returns>Unchanged result collection</returns>     
        public static async Task<IRList<TValue>> RListVoidMatchAwait<TValue>(this Task<IRList<TValue>> @this,
                                                                                                     Func<IReadOnlyCollection<TValue>, Task> actionSome,
                                                                                                     Func<IReadOnlyCollection<IRError>, Task> actionNone)
            where TValue : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.RListVoidMatchAsync(actionSome, actionNone));

        /// <summary>
        /// Execute async action depending on task result collection errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result collection</returns>
        public static async Task<IRList<TValue>> RListVoidOptionAwait<TValue>(this Task<IRList<TValue>> @this,
                                                                                          Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                                          Func<IReadOnlyCollection<TValue>, Task> action)
            where TValue : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.RListVoidOptionAsync(predicate, action));
    }
}