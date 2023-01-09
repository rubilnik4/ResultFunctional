using System;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors.ResultErrorTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors
{
    /// <summary>
    /// Exception handling result error with conditions extension methods
    /// </summary>
    public static class ResultErrorTryWhereExtensions
    {
        /// <summary>
        /// Execute function and handle exception with result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result error</returns>
        public static IResultError ResultErrorTryOk(this IResultError @this, Action action,
                                                    Func<Exception, IRError> exceptionFunc) =>
            @this.ResultErrorBindOk(() => ResultErrorTry(action.Invoke, exceptionFunc));

        /// <summary>
        /// Execute function and handle exception with result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result error</returns>
        public static IResultError ResultErrorTryOk(this IResultError @this, Action action, IRError error) =>
            @this.ResultErrorTryOk(action, _ => error);
    }
}