using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Units
{
    /// <summary>
    /// Exception handling task result error with conditions extension methods
    /// </summary>
    public static class ResultErrorTryWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with task result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRUnit> ResultErrorTryOkTaskAsync(this Task<IRUnit> @this, Action action,
                                                                     Func<Exception, IRError> exceptionFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultErrorTryOk(action, exceptionFunc));

        /// <summary>
        /// Execute async function and handle exception with task result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRUnit> ResultErrorTryOkTaskAsync(this Task<IRUnit> @this, Action action,
                                                                         IRError error) =>
            await @this.ResultErrorTryOkTaskAsync(action, _ => error);
    }
}