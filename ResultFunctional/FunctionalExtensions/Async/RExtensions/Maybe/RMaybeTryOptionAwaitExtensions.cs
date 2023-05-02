using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe
{
    /// <summary>
    /// Async exception handling task result error with conditions extension methods
    /// </summary>
    public static class RMaybeTryOptionAwaitExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with task result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRMaybe> RMaybeTrySomeAwait(this Task<IRMaybe> @this, Func<Task> action,
                                                           Func<Exception, IRError> exceptionFunc) =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.RMaybeTrySomeAsync(action, exceptionFunc));

        /// <summary>
        /// Execute async function and handle exception with task result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRMaybe> RMaybeTrySomeAwait(this Task<IRMaybe> @this, Func<Task> action, IRError error) =>
            await @this.RMaybeTrySomeAwait(action, _ => error);
    }
}