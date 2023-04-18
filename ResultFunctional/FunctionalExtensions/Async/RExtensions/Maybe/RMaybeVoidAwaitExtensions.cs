using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe
{
    /// <summary>
    /// Task result error async action extension methods
    /// </summary>
    public static class RMaybeVoidAwaitExtensions
    {
        /// <summary>
        /// Execute async action if task result hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IRMaybe> RMaybeVoidSomeAwait(this Task<IRMaybe> @this, Func<Task> action) =>
            await @this.
                VoidSomeAwait(awaitedThis => awaitedThis.Success,
                              _ => action.Invoke());

        /// <summary>
        /// Execute async action if task result has errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IRMaybe> RMaybeVoidNoneAwait(this Task<IRMaybe> @this,
                                                             Func<IReadOnlyCollection<IRError>, Task> action) =>
            await @this.
                VoidSomeAwait(awaitedThis => awaitedThis.Failure,
                              awaitedThis => action.Invoke(awaitedThis.GetErrors()));

        /// <summary>
        /// Execute async action depending on task result errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="actionSome">Action if result hasn't errors</param>
        /// <param name="actionNone">Action if result has errors</param>
        /// <returns>Unchanged result error</returns>  
        public static async Task<IRMaybe> RMaybeVoidMatchAwait(this Task<IRMaybe> @this, Func<Task> actionSome,
                                                               Func<IReadOnlyCollection<IRError>, Task> actionNone) =>
            await @this.
                VoidOptionAwait(awaitedThis => awaitedThis.Success,
                                _ => actionSome.Invoke(),
                                awaitedThis => actionNone.Invoke(awaitedThis.GetErrors()));

        /// <summary>
        /// Execute async action depending on task result errors and predicate
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result error</returns>  
        public static async Task<IRMaybe> RMaybeVoidWhereAwait(this Task<IRMaybe> @this, Func<bool> predicate,
                                                               Func<Task> action) =>
            await @this.
                VoidSomeAwait(awaitedThis => awaitedThis.Success && predicate(),
                              _ => action.Invoke());
    }
}