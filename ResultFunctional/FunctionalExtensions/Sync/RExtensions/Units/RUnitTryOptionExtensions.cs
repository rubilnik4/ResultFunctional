using System;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;
using static ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units.RUnitTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units
{
    /// <summary>
    /// Exception handling result error with conditions extension methods
    /// </summary>
    public static class RUnitTryOptionExtensions
    {
        /// <summary>
        /// Execute function and handle exception with result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result error</returns>
        public static IRUnit RUnitTrySome(this IRUnit @this, Action action,
                                          Func<Exception, IRError> exceptionFunc) =>
            @this.RUnitBindSome(() => RUnitTry(action.Invoke, exceptionFunc));

        /// <summary>
        /// Execute function and handle exception with result error concat
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result error</returns>
        public static IRUnit RUnitTrySome(this IRUnit @this, Action action, IRError error) =>
            @this.RUnitTrySome(action, _ => error);
    }
}