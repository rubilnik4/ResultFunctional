using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Units
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
        /// <param name="someFunc">Function if result error hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRUnit> ResultErrorBindOkBadBindAsync(this Task<IRUnit> @this,
                                                             Func<Task<IRUnit>> someFunc,
                                                             Func<IReadOnlyCollection<IRError>, Task<IRUnit>> noneFunc) =>
           await @this.
           MapAwait(awaitedThis => awaitedThis.ResultErrorBindOkBadAsync(someFunc, noneFunc));

        /// <summary>
        /// Merge task result error async function if incoming result collection hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="someFunc">Function if result error hasn't errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRUnit> ResultErrorBindOkBindAsync(this Task<IRUnit> @this, Func<Task<IRUnit>> someFunc) =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.ResultErrorBindOkAsync(someFunc));
    }
}