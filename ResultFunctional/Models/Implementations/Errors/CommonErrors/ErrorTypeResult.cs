using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.CommonErrors
{
    /// <summary>
    /// Error with error subtype
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    public class ErrorTypeResult<TErrorType> : ErrorBaseResult<TErrorType, ErrorTypeResult<TErrorType>>
        where TErrorType : struct
    {
        /// <summary>
        /// Initialize error with common error subtype
        /// </summary>
        /// <param name="errorType">Error subtype</param>
        /// <param name="description">Description</param>
        public ErrorTypeResult(TErrorType errorType, string description)
            : this(errorType, description, null)
        { }

        /// <summary>
        /// Initialize error with common error subtype
        /// </summary>
        /// <param name="errorType">Error subtype</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        public ErrorTypeResult(TErrorType errorType, string description, Exception? exception)
            : base(errorType, description, exception)
        { }

        /// <summary>
        /// Initialize error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Common error</returns>
        protected override ErrorTypeResult<TErrorType> InitializeType(string description, Exception? exception) =>
            new ErrorTypeResult<TErrorType>(ErrorType, description, exception);
    }
}