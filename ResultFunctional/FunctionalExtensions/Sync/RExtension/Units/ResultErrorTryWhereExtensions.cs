using System;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;
using static ResultFunctional.FunctionalExtensions.Sync.RExtension.Units.ResultErrorTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Units
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
        public static IRUnit ResultErrorTryOk(this IRUnit @this, Action action,
                                              Func<Exception, IRError> exceptionFunc) =>
            @this.ResultErrorBindOk(() => ResultErrorTry(action.Invoke, exceptionFunc));

        /// <summary>
        /// Execute function and handle exception with result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result error</returns>
        public static IRUnit ResultErrorTryOk(this IRUnit @this, Action action, IRError error) =>
            @this.ResultErrorTryOk(action, _ => error);
    }
}