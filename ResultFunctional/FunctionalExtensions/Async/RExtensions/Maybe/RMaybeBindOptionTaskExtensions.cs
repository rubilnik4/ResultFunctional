using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe
{
    /// <summary>
    /// Task result error extension methods for merging errors
    /// </summary>
    public static class RMaybeBindOptionTaskExtensions
    {
        /// <summary>
        /// Merge task result error function depending on incoming result error
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="someFunc">Function if result error hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRMaybe> RMaybeBindMatchTask(this Task<IRMaybe> @this, Func<IRMaybe> someFunc,
                                                             Func<IReadOnlyCollection<IRError>, IRMaybe> noneFunc) =>
           await @this.
           MapTask(awaitedThis => awaitedThis.RMaybeBindMatch(someFunc, noneFunc));

        /// <summary>
        /// Merge task result error function if incoming result collection hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="someFunc">Function if result error hasn't errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRMaybe> RMaybeBindSomeTask(this Task<IRMaybe> @this, Func<IRMaybe> someFunc) =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RMaybeBindSome(someFunc));
    }
}