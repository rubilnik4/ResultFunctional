using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Task result error async action extension methods
    /// </summary>
    public static class ResultErrorVoidBindAsyncExtensions
    {
        /// <summary>
        /// Execute async action if task result hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IResultError> ResultErrorVoidOkBindAsync(this Task<IResultError> @this, Func<Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.OkStatus,
                            _ => action.Invoke());

        /// <summary>
        /// Execute async action if task result has errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IResultError> ResultErrorVoidBadBindAsync(this Task<IResultError> @this,
                                                                       Func<IReadOnlyCollection<IRError>, Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.HasErrors,
                            awaitedThis => action.Invoke(awaitedThis.Errors));

        /// <summary>
        /// Execute async action depending on task result errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="actionOk">Action if result hasn't errors</param>
        /// <param name="actionBad">Action if result has errors</param>
        /// <returns>Unchanged result error</returns>  
        public static async Task<IResultError> ResultErrorVoidOkBadBindAsync(this Task<IResultError> @this,
                                                                         Func<Task> actionOk,
                                                                         Func<IReadOnlyCollection<IRError>, Task> actionBad) =>
            await @this.
            VoidWhereBindAsync(awaitedThis => awaitedThis.OkStatus,
                               _ => actionOk.Invoke(),
                               awaitedThis => actionBad.Invoke(awaitedThis.Errors));

        /// <summary>
        /// Execute async action depending on task result errors and predicate
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result error</returns>  
        public static async Task<IResultError> ResultErrorVoidOkWhereBindAsync(this Task<IResultError> @this,
                                                                           Func<bool> predicate,
                                                                           Func<Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.OkStatus && predicate(),
                            _ => action.Invoke());
    }
}