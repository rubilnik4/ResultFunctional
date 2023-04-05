using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Result error async extension methods for merging errors
    /// </summary>
    public static class ResultErrorBindWhereAsyncExtensions
    {
        /// <summary>
        /// Merge result error async function depending on incoming result error
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="okFunc">Function if result error hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IResultError> ResultErrorBindOkBadAsync(this IResultError @this,
                                                             Func<Task<IResultError>> okFunc,
                                                             Func<IReadOnlyCollection<IRError>, Task<IResultError>> badFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke()
                : await badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Merge result error async function if incoming result collection hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="okFunc">Function if result error hasn't errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IResultError> ResultErrorBindOkAsync(this IResultError @this, Func<Task<IResultError>> okFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke()
                : @this;
    }
}