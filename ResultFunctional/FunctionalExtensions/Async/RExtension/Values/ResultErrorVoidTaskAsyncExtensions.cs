using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Values
{
    /// <summary>
    /// Task result error action extension methods
    /// </summary>
    public static class ResultErrorVoidTaskAsyncExtensions
    {
        /// <summary>
        /// Execute action if task result hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns> 
        public static async Task<IResultError> ResultErrorVoidOkTaskAsync(this Task<IResultError> @this, Action action) =>
            await @this.
            VoidOkTaskAsync(awaitedThis => awaitedThis.OkStatus,
                action: _ => action.Invoke());

        /// <summary>
        /// Execute action if task result has errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IResultError> ResultErrorVoidBadTaskAsync(this Task<IResultError> @this,
                                                                       Action<IReadOnlyCollection<IRError>> action) =>
            await @this.
            VoidOkTaskAsync(awaitedThis => awaitedThis.HasErrors,
                action: awaitedThis => action.Invoke(awaitedThis.Errors));

        /// <summary>
        /// Execute action depending on task result errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="actionOk">Action if result hasn't errors</param>
        /// <param name="actionBad">Action if result has errors</param>
        /// <returns>Unchanged result error</returns>   
        public static async Task<IResultError> ResultErrorVoidOkBadTaskAsync(this Task<IResultError> @this, Action actionOk,
                                                                             Action<IReadOnlyCollection<IRError>> actionBad) =>
            await @this.
            VoidWhereTaskAsync(awaitedThis => awaitedThis.OkStatus,
                actionOk: _ => actionOk.Invoke(),
                actionBad: awaitedThis => actionBad.Invoke(awaitedThis.Errors));

        /// <summary>
        /// Execute action depending on task result errors and predicate
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result error</returns>  
        public static async Task<IResultError> ResultErrorVoidOkWhereTaskAsync(this Task<IResultError> @this,
                                                                           Func<bool> predicate,
                                                                           Action action) =>
            await @this.
            VoidOkTaskAsync(awaitedThis=> awaitedThis.OkStatus && predicate(),
                action: _ => action.Invoke());
    }
}