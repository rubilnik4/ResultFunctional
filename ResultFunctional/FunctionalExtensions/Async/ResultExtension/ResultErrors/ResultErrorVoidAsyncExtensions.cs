using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Result error async action extension methods
    /// </summary>
    public static class ResultErrorVoidAsyncExtensions
    {
        /// <summary>
        /// Execute async action if result hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IResultError> ResultErrorVoidOkAsync(this IResultError @this, Func<Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.OkStatus,
                        _ => action.Invoke());

        /// <summary>
        /// Execute async action if result has errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IResultError> ResultErrorVoidBadAsync(this IResultError @this,
                                                                       Func<IReadOnlyCollection<IRError>, Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.HasErrors,
                        _ => action.Invoke(@this.Errors));

        /// <summary>
        /// Execute async action depending on result errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="actionOk">Action if result hasn't errors</param>
        /// <param name="actionBad">Action if result has errors</param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IResultError> ResultErrorVoidOkBadAsync(this IResultError @this,
                                                                         Func<Task> actionOk,
                                                                         Func<IReadOnlyCollection<IRError>, Task> actionBad) =>
            await @this.
            VoidWhereAsync(_ => @this.OkStatus,
                           _ => actionOk.Invoke(),
                           _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Execute async action depending on result errors and predicate
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IResultError> ResultErrorVoidOkWhereAsync(this IResultError @this,
                                                                           Func<bool> predicate,
                                                                           Func<Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.OkStatus && predicate(),
                        _ => action.Invoke());
    }
}