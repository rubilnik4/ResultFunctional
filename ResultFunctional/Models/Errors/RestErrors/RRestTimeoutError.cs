using System;
using ResultFunctional.Models.Enums;

namespace ResultFunctional.Models.Errors.RestErrors
{
    /// <summary>
    /// Timeout rest error
    /// </summary>
    public class RRestTimeoutError : RRestError<RRestTimeoutError>
    {
        /// <summary>
        /// Initialize timeout rest error
        /// </summary>
        /// <param name="host">Host</param>
        /// <param name="timeout">Timeout</param>
        /// <param name="description">Description</param>
        public RRestTimeoutError(string host, TimeSpan timeout, string description)
            : this(host, timeout, description, null)
        { }

        /// <summary>
        /// Initialize timeout rest error
        /// </summary>
        /// <param name="host">Host</param>
        /// <param name="timeout">Timeout</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RRestTimeoutError(string host, TimeSpan timeout, string description, Exception? exception)
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
        protected override RRestTimeoutError InitializeType(string description, Exception? exception) =>
            new(Host, Timeout, description, exception);
    }
}