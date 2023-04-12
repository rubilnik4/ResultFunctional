using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Units
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
        public static async Task<IRUnit> ResultErrorVoidOkBindAsync(this Task<IRUnit> @this, Func<Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.Success,
                            _ => action.Invoke());

        /// <summary>
        /// Execute async action if task result has errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IRUnit> ResultErrorVoidBadBindAsync(this Task<IRUnit> @this,
                                                                       Func<IReadOnlyCollection<IRError>, Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.Failure,
                            awaitedThis => action.Invoke(awaitedThis.GetErrors()));

        /// <summary>
        /// Execute async action depending on task result errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="actionSome">Action if result hasn't errors</param>
        /// <param name="actionNone">Action if result has errors</param>
        /// <returns>Unchanged result error</returns>  
        public static async Task<IRUnit> ResultErrorVoidOkBadBindAsync(this Task<IRUnit> @this,
                                                                         Func<Task> actionSome,
                                                                         Func<IReadOnlyCollection<IRError>, Task> actionNone) =>
            await @this.
            VoidWhereBindAsync(awaitedThis => awaitedThis.Success,
                               _ => actionSome.Invoke(),
                               awaitedThis => actionNone.Invoke(awaitedThis.GetErrors()));

        /// <summary>
        /// Execute async action depending on task result errors and predicate
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result error</returns>  
        public static async Task<IRUnit> ResultErrorVoidOkWhereBindAsync(this Task<IRUnit> @this,
                                                                           Func<bool> predicate,
                                                                           Func<Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.Success && predicate(),
                            _ => action.Invoke());
    }
}