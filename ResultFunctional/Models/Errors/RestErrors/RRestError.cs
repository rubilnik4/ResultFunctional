using System;
using System.Net;
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
        /// <param name="restErrorTyper">Rest error type</param>
        /// <param name="description">Description</param>
        protected RRestError(RestErrorType restErrorTyper, string description)
          : this(restErrorTyper, description, null)
        { }

        /// <summary>
        /// Initialize rest error
        /// </summary>
        /// <param name="restErrorTyper">Rest error type</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RRestError(RestErrorType restErrorTyper, string description, Exception? exception)
            : base(restErrorTyper, description, exception)
        { }
    }
}