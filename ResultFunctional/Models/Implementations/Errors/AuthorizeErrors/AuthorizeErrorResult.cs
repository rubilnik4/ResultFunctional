using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Errors.RestErrors;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.AuthorizeErrors
{
    /// <summary>
    /// Authorize error
    /// </summary>
    public class AuthorizeErrorResult: ErrorBaseResult<AuthorizeErrorType, AuthorizeErrorResult>
    {
        /// <summary>
        /// Initialize authorize error
        /// </summary>
        /// <param name="authorizeErrorType">Authorize error type</param>
        /// <param name="description">Description</param>
        public AuthorizeErrorResult(AuthorizeErrorType authorizeErrorType, string description)
            : this(authorizeErrorType, description, null)
        { }

        /// <summary>
        /// Initialize authorize error
        /// </summary>
        /// <param name="authorizeErrorType">Authorize error type</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected AuthorizeErrorResult(AuthorizeErrorType authorizeErrorType, string description, Exception? exception)
            : base(authorizeErrorType, description, exception)
        { }

        /// <summary>
        /// Initialize error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Authorize error</returns>
        protected override AuthorizeErrorResult InitializeType(string description, Exception? exception) =>
            new AuthorizeErrorResult(ErrorType, description, exception);
    }
}