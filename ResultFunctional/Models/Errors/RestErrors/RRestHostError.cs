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
    public class RRestHostError : RSimpleError, IRRestError
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
            : base(description, exception)
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
        protected override IRError Initialize(string description, Exception? exception) =>
            new RRestHostError(Host, description, exception);
    }
}