using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;
using static ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe.RMaybeTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe
{
    /// <summary>
    /// Async exception handling result error with conditions extension methods
    /// </summary>
    public static class RMaybeTryOptionAsyncExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRMaybe> RMaybeTrySomeAsync(this IRMaybe @this, Func<Task> action,
                                                           Func<Exception, IRError> exceptionFunc) =>
            await @this.RMaybeBindSomeAsync(() => RMaybeTryAsync(action.Invoke, exceptionFunc));

        /// <summary>
        /// Execute async function and handle exception with result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRMaybe> RMaybeTrySomeAsync(this IRMaybe @this, Func<Task> action, IRError error) =>
            await @this.RMaybeTrySomeAsync(action, _ => error);
    }
}