using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Errors.CommonErrors
{
    /// <summary>
    /// Error with common error subtype
    /// </summary>
    public class RCommonError : RBaseError<CommonErrorType, RCommonError>
    {
        /// <summary>
        /// Initialize error with common error subtype
        /// </summary>
        /// <param name="commonErrorType">Common error subtype</param>
        /// <param name="description">Description</param>
        public RCommonError(CommonErrorType commonErrorType, string description)
            : this(commonErrorType, description, null)
        { }

        /// <summary>
        /// Initialize error with common error subtype
        /// </summary>
        /// <param name="commonErrorType">Common error subtype</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        public RCommonError(CommonErrorType commonErrorType, string description, Exception? exception)
            : base(commonErrorType, description, exception)
        { }

        /// <summary>
        /// Initialize error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Common error</returns>
        protected override RCommonError InitializeType(string description, Exception? exception) =>
            new (ErrorType, description, exception);
    }
}