using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.RestErrors;

namespace ResultFunctional.Models.Implementations.Errors.RestErrors
{
    /// <summary>
    /// Rest error
    /// </summary>
    /// <typeparam name="TErrorResult">Rest error type</typeparam>
    public abstract class RestErrorResult<TErrorResult> : ErrorBaseResult<RestErrorType, TErrorResult>, IRestErrorResult
        where TErrorResult : RestErrorResult<TErrorResult>
    {
        /// <summary>
        /// Initialize rest error
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="description">Description</param>
        protected RestErrorResult(RestErrorType restErrorType, string description)
          : this(restErrorType, description, null)
        { }

        /// <summary>
        /// Initialize rest error
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RestErrorResult(RestErrorType restErrorType, string description, Exception? exception)
            : base(restErrorType, description, exception)
        { }
    }
}