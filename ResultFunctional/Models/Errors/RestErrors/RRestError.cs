using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.RestErrors
{
    /// <summary>
    /// Rest error
    /// </summary>
    /// <typeparam name="TError">Rest error type</typeparam>
    public abstract class RRestError<TError> : RBaseError<RestErrorType, TError>, IRRestError
        where TError : RRestError<TError>
    {
        /// <summary>
        /// Initialize rest error
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="description">Description</param>
        protected RRestError(RestErrorType restErrorType, string description)
          : this(restErrorType, description, null)
        { }

        /// <summary>
        /// Initialize rest error
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RRestError(RestErrorType restErrorType, string description, Exception? exception)
            : base(restErrorType, description, exception)
        { }
    }
}