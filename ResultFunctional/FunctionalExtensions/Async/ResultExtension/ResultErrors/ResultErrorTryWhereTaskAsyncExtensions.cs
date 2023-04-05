using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors.ResultErrorTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
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
        public static async Task<IResultError> ResultErrorTryOkTaskAsync(this Task<IResultError> @this, Action action,
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
        public static async Task<IResultError> ResultErrorTryOkTaskAsync(this Task<IResultError> @this, Action action,
                                                                         IRError error) =>
            await @this.ResultErrorTryOkTaskAsync(action, _ => error);
    }
}