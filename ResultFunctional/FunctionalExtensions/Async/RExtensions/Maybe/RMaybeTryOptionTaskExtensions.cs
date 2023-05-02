using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe
{
    /// <summary>
    /// Exception handling task result error with conditions extension methods
    /// </summary>
    public static class RMaybeTryOptionTaskExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with task result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRMaybe> RMaybeTrySomeTask(this Task<IRMaybe> @this, Action action,
                                                            Func<Exception, IRError> exceptionFunc) =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RMaybeTrySome(action, exceptionFunc));

        /// <summary>
        /// Execute async function and handle exception with task result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRMaybe> RMaybeTrySomeTask(this Task<IRMaybe> @this, Action action, IRError error) =>
            await @this.RMaybeTrySomeTask(action, _ => error);
    }
}