using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Units
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
        public static async Task<IRUnit> ResultErrorVoidOkAsync(this IRUnit @this, Func<Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.Success,
                        _ => action.Invoke());

        /// <summary>
        /// Execute async action if result has errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IRUnit> ResultErrorVoidBadAsync(this IRUnit @this,
                                                                       Func<IReadOnlyCollection<IRError>, Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.Failure,
                        _ => action.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute async action depending on result errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="actionOk">Action if result hasn't errors</param>
        /// <param name="actionBad">Action if result has errors</param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IRUnit> ResultErrorVoidOkBadAsync(this IRUnit @this,
                                                                         Func<Task> actionOk,
                                                                         Func<IReadOnlyCollection<IRError>, Task> actionBad) =>
            await @this.
            VoidWhereAsync(_ => @this.Success,
                           _ => actionOk.Invoke(),
                           _ => actionBad.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute async action depending on result errors and predicate
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IRUnit> ResultErrorVoidOkWhereAsync(this IRUnit @this,
                                                                           Func<bool> predicate,
                                                                           Func<Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.Success && predicate(),
                        _ => action.Invoke());
    }
}