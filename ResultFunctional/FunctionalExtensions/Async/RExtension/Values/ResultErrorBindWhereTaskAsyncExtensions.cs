using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Values
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
        public static async Task<IRUnit> ResultErrorBindOkBadTaskAsync(this Task<IRUnit> @this, Func<IRUnit> okFunc,
                                                             Func<IReadOnlyCollection<IRError>, IRUnit> badFunc) =>
           await @this.
           MapTaskAsync(awaitedThis => awaitedThis.ResultErrorBindOkBad(okFunc, badFunc));

        /// <summary>
        /// Merge task result error function if incoming result collection hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="okFunc">Function if result error hasn't errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRUnit> ResultErrorBindOkTaskAsync(this Task<IRUnit> @this,
                                                                          Func<IRUnit> okFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultErrorBindOk(okFunc));
    }
}