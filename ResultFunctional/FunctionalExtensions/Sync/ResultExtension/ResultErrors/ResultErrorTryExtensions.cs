using System;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors
{
    /// <summary>
    /// Exception handling result error extension methods
    /// </summary>
    public static class ResultErrorTryExtensions
    {
        /// <summary>
        /// Execute action and handle exception with result error converting
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Result error</returns>
        public static IResultError ResultErrorTry(Action action, Func<Exception, IErrorResult> exceptionFunc)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                return new ResultError(exceptionFunc(ex));
            }

            return new ResultError();
        }

        /// <summary>
        /// Execute action and handle exception with result error converting
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Result error</returns>
        public static IResultError ResultErrorTry(Action action, IErrorResult error) =>
            ResultErrorTry(action, error.AppendException);
    }
}