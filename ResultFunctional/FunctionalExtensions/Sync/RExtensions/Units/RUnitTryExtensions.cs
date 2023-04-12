using System;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units
{
    /// <summary>
    /// Exception handling result error extension methods
    /// </summary>
    public static class RUnitTryExtensions
    {
        /// <summary>
        /// Execute action and handle exception with result error converting
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Result error</returns>
        public static IRUnit RUnitTry(Action action, Func<Exception, IRError> exceptionFunc)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                return exceptionFunc(ex).ToRUnit();
            }

            return RUnitFactory.Some();
        }

        /// <summary>
        /// Execute action and handle exception with result error converting
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="error">Error</param>
        /// <returns>Result error</returns>
        public static IRUnit RUnitTry(Action action, IRError error) =>
            RUnitTry(action, error.AppendException);
    }
}