using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Task result error async extension methods for merging errors
    /// </summary>
    public static class ResultErrorBindWhereBindAsyncExtensions
    {
        /// <summary>
        /// Merge task result error async function depending on incoming result error
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="okFunc">Function if result error hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IResultError> ResultErrorBindOkBadBindAsync(this Task<IResultError> @this,
                                                             Func<Task<IResultError>> okFunc,
                                                             Func<IReadOnlyCollection<IRError>, Task<IResultError>> badFunc) =>
           await @this.
           MapBindAsync(awaitedThis => awaitedThis.ResultErrorBindOkBadAsync(okFunc, badFunc));

        /// <summary>
        /// Merge task result error async function if incoming result collection hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="okFunc">Function if result error hasn't errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IResultError> ResultErrorBindOkBindAsync(this Task<IResultError> @this, Func<Task<IResultError>> okFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultErrorBindOkAsync(okFunc));
    }
}