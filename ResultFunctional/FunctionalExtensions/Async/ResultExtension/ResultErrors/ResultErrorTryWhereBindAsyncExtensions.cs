using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors.ResultErrorTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Async exception handling task result error with conditions extension methods
    /// </summary>
    public static class ResultErrorTryWhereBindAsyncExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with task result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IResultError> ResultErrorTryOkBindAsync(this Task<IResultError> @this, Func<Task> action,
                                                                     Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultErrorTryOkAsync(action, exceptionFunc));

        /// <summary>
        /// Execute async function and handle exception with task result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IResultError> ResultErrorTryOkBindAsync(this Task<IResultError> @this, Func<Task> action,
                                                                         IErrorResult error) =>
            await @this.ResultErrorTryOkBindAsync(action, _ => error);
    }
}