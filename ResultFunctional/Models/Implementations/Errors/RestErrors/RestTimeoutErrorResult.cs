using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.RestErrors
{
    /// <summary>
    /// Timeout rest error
    /// </summary>
    public class RestTimeoutErrorResult : RestErrorResult<RestTimeoutErrorResult>
    {
        /// <summary>
        /// Initialize timeout rest error
        /// </summary>
        /// <param name="host">Host</param>
        /// <param name="timeout">Timeout</param>
        /// <param name="description">Description</param>
        public RestTimeoutErrorResult(string host, TimeSpan timeout, string description)
            : this(host, timeout, description, null)
        { }

        /// <summary>
        /// Initialize timeout rest error
        /// </summary>
        /// <param name="host">Host</param>
        /// <param name="timeout">Timeout</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RestTimeoutErrorResult(string host, TimeSpan timeout, string description, Exception? exception)
            : base(RestErrorType.RequestTimeout, description, exception)
        {
            Host = host;
            Timeout = timeout;
        }

        /// <summary>
        /// Host
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Timeout
        /// </summary>
        public TimeSpan Timeout { get; }

        /// <summary>
        /// Initialize timeout rest error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected override RestTimeoutErrorResult InitializeType(string description, Exception? exception) =>
            new RestTimeoutErrorResult(Host, Timeout, description, exception);
    }
}