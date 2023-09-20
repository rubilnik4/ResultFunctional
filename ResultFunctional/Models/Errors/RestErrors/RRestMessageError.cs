using System;
using System.Net;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.RestErrors
{
    /// <summary>
    /// Rest error with response message
    /// </summary>
    public class RRestMessageError : RBaseError<HttpStatusCode, RRestMessageError>, IRRestError
    {
        /// <summary>
        /// Initialize rest error with response message
        /// </summary>
        /// <param name="httpStatusCode">Http status code</param>
        /// <param name="reasonPhrase">Reason phrase</param>
        /// <param name="description">Description</param>
        public RRestMessageError(HttpStatusCode httpStatusCode, string reasonPhrase, string description)
            : this(httpStatusCode, reasonPhrase, description, null)
        { }

        /// <summary>
        /// Initialize rest error with response message
        /// </summary>
        /// <param name="httpStatusCode">Http status code</param>
        /// <param name="reasonPhrase">Reason phrase</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RRestMessageError(HttpStatusCode httpStatusCode, string reasonPhrase, string description, Exception? exception)
            : base(httpStatusCode, description, exception)
        {
            ReasonPhrase = reasonPhrase;
        }

        /// <summary>
        /// Reason phrase
        /// </summary>
        public string ReasonPhrase { get; }

        /// <summary>
        /// Http status code
        /// </summary>
        public HttpStatusCode HttpStatusCode =>
            ErrorType;

        /// <summary>
        /// Initialize rest error with response message
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected override RRestMessageError InitializeType(string description, Exception? exception) =>
            new(HttpStatusCode, ReasonPhrase, description, exception);
    }
}