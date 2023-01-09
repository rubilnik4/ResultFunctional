using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Task result error extension methods for merging errors
    /// </summary>
    public static class ResultErrorBindWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Merge task result error function depending on incoming result error
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="okFunc">Function if result error hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IResultError> ResultErrorBindOkBadTaskAsync(this Task<IResultError> @this,
                                                             Func<IResultError> okFunc,
                                                             Func<IReadOnlyCollection<IRError>, IResultError> badFunc) =>
           await @this.
           MapTaskAsync(awaitedThis => awaitedThis.ResultErrorBindOkBad(okFunc, badFunc));

        /// <summary>
        /// Merge task result error function if incoming result collection hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="okFunc">Function if result error hasn't errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IResultError> ResultErrorBindOkTaskAsync(this Task<IResultError> @this,
                                                                          Func<IResultError> okFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultErrorBindOk(okFunc));
    }
}