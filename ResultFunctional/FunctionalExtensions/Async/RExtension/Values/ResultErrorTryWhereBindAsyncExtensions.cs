using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Values
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
                                                                     Func<Exception, IRError> exceptionFunc) =>
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
                                                                         IRError error) =>
            await @this.ResultErrorTryOkBindAsync(action, _ => error);
    }
}