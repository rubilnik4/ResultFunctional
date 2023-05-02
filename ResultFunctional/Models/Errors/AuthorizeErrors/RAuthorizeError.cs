using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.AuthorizeErrors
{
    /// <summary>
    /// Authorize error
    /// </summary>
    public class RAuthorizeError: RBaseError<AuthorizeErrorType, RAuthorizeError>
    {
        /// <summary>
        /// Initialize authorize error
        /// </summary>
        /// <param name="authorizeErrorType">Authorize error type</param>
        /// <param name="description">Description</param>
        public RAuthorizeError(AuthorizeErrorType authorizeErrorType, string description)
            : this(authorizeErrorType, description, null)
        { }

        /// <summary>
        /// Initialize authorize error
        /// </summary>
        /// <param name="authorizeErrorType">Authorize error type</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RAuthorizeError(AuthorizeErrorType authorizeErrorType, string description, Exception? exception)
            : base(authorizeErrorType, description, exception)
        { }

        /// <summary>
        /// Initialize error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Authorize error</returns>
        protected override RAuthorizeError InitializeType(string description, Exception? exception) =>
            new(ErrorType, description, exception);
    }
}