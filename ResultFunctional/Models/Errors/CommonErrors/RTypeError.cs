using System;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.CommonErrors
{
    /// <summary>
    /// Error with error subtype
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    public class RTypeError<TErrorType> : RBaseError<TErrorType, RTypeError<TErrorType>>
        where TErrorType : struct
    {
        /// <summary>
        /// Initialize error with common error subtype
        /// </summary>
        /// <param name="errorType">Error subtype</param>
        /// <param name="description">Description</param>
        public RTypeError(TErrorType errorType, string description)
            : this(errorType, description, null)
        { }

        /// <summary>
        /// Initialize error with common error subtype
        /// </summary>
        /// <param name="errorType">Error subtype</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        public RTypeError(TErrorType errorType, string description, Exception? exception)
            : base(errorType, description, exception)
        { }

        /// <summary>
        /// Initialize error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Common error</returns>
        protected override RTypeError<TErrorType> InitializeType(string description, Exception? exception) =>
            new(ErrorType, description, exception);
    }
}