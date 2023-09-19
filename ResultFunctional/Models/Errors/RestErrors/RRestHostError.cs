using System;
using System.Net;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Errors.CommonErrors;

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
        /// <param name="host">Host</param>
        /// <param name="description">Description</param>
        public RRestHostError(string host, string description)
            : this(host, description, null)
        { }

        /// <summary>
        /// Initialize host connection error
        /// </summary>
        /// <param name="host">Host</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RRestHostError(string host, string description, Exception? exception)
            : base(RestErrorType.Host, description, exception)
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
            new(Host, description, exception);
    }
}