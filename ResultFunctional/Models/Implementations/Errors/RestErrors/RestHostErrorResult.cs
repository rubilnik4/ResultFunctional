using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.RestErrors
{
    /// <summary>
    /// Host connection error
    /// </summary>
    public class RestHostErrorResult : RestErrorResult<RestHostErrorResult>
    {
        /// <summary>
        /// Initialize host connection error
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="host">Host</param>
        /// <param name="description">Description</param>
        public RestHostErrorResult(RestErrorType restErrorType, string host, string description)
            : this(restErrorType, host, description, null)
        { }

        /// <summary>
        /// Initialize host connection error
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="host">Host</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RestHostErrorResult(RestErrorType restErrorType, string host, string description, Exception? exception)
            : base(restErrorType, description, exception)
        {
            Host = host;
        }

        /// <summary>
        /// Host
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Initialize host connection error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected override RestHostErrorResult InitializeType(string description, Exception? exception) =>
            new RestHostErrorResult(ErrorType, Host, description, exception);
    }
}