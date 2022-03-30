using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.CommonErrors
{
    /// <summary>
    /// Error with common error subtype
    /// </summary>
    public class CommonErrorResult : ErrorBaseResult<CommonErrorType, CommonErrorResult>
    {
        /// <summary>
        /// Initialize error with common error subtype
        /// </summary>
        /// <param name="commonErrorType">Common error subtype</param>
        /// <param name="description">Description</param>
        public CommonErrorResult(CommonErrorType commonErrorType, string description)
            : this(commonErrorType, description, null)
        { }

        /// <summary>
        /// Initialize error with common error subtype
        /// </summary>
        /// <param name="commonErrorType">Common error subtype</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        public CommonErrorResult(CommonErrorType commonErrorType, string description, Exception? exception)
            : base(commonErrorType, description, exception)
        { }

        /// <summary>
        /// Initialize error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Common error</returns>
        protected override CommonErrorResult InitializeType(string description, Exception? exception) =>
            new CommonErrorResult(ErrorType, description, exception);
    }
}