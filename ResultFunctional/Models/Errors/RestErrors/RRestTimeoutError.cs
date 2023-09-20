using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Errors.CommonErrors;

namespace ResultFunctional.Models.Errors.RestErrors
{
    /// <summary>
    /// Timeout rest error
    /// </summary>
    public class RRestTimeoutError : RSimpleError, IRRestError
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
            : base(description, exception)
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
        protected override IRError Initialize(string description, Exception? exception) =>
            new RRestTimeoutError(Host, Timeout, description, exception);
    }
}