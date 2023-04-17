using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Units
{
    /// <summary>
    /// Task result error extension methods for merging errors
    /// </summary>
    public static class RUnitBindOptionTaskExtensions
    {
        /// <summary>
        /// Merge task result error function depending on incoming result error
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="someFunc">Function if result error hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRUnit> RUnitBindMatchTask(this Task<IRUnit> @this, Func<IRUnit> someFunc,
                                                            Func<IReadOnlyCollection<IRError>, IRUnit> noneFunc) =>
           await @this.
           MapTask(awaitedThis => awaitedThis.RUnitBindMatch(someFunc, noneFunc));

        /// <summary>
        /// Merge task result error function if incoming result collection hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="someFunc">Function if result error hasn't errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRUnit> RUnitBindSomeTask(this Task<IRUnit> @this, Func<IRUnit> someFunc) =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RUnitBindSome(someFunc));
    }
}