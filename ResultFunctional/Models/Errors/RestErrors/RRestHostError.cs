using System;
using ResultFunctional.Models.Enums;

namespace ResultFunctional.Models.Errors.RestErrors
{
    /// <summary>
    /// Host connection error
    /// </summary>
    public class RRestHostError : RRestError<RRestHostError>
    {
        /// <summary>
        /// Initialize host connection error
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="host">Host</param>
        /// <param name="description">Description</param>
        public RRestHostError(RestErrorType restErrorType, string host, string description)
            : this(restErrorType, host, description, null)
        { }

        /// <summary>
        /// Initialize host connection error
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="host">Host</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RRestHostError(RestErrorType restErrorType, string host, string description, Exception? exception)
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
        protected override RRestHostError InitializeType(string description, Exception? exception) =>
            new(ErrorType, Host, description, exception);
    }
}