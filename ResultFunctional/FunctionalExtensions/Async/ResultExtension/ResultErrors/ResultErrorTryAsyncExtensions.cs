using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Exception handling result error async extension methods
    /// </summary>
    public static class ResultErrorTryAsyncExtensions
    {
        /// <summary>
        /// Execute async action and handle exception with result error converting
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Result error</returns>
        public static async Task<IResultError> ResultErrorTryAsync(Func<Task> action, Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                await action.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultError(exceptionFunc(ex));
            }

            return new ResultError();
        }

        /// <summary>
        /// Execute async action and handle exception with result error converting
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Result error</returns>
        public static async Task<IResultError> ResultErrorTryAsync(Func<Task> action, IErrorResult error) =>
            await ResultErrorTryAsync(action, error.AppendException);
    }
}