using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors.ResultErrorTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Async exception handling result error with conditions extension methods
    /// </summary>
    public static class ResultErrorTryWhereAsyncExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IResultError> ResultErrorTryOkAsync(this IResultError @this, Func<Task> action,
                                                                     Func<Exception, IRError> exceptionFunc) =>
            await @this.ResultErrorBindOkAsync(() => ResultErrorTryAsync(action.Invoke, exceptionFunc));

        /// <summary>
        /// Execute async function and handle exception with result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IResultError> ResultErrorTryOkAsync(this IResultError @this, Func<Task> action, IRError error) =>
            await @this.ResultErrorTryOkAsync(action, _ => error);
    }
}