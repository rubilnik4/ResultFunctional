using System;
using System.Net;
using ResultFunctional.Models.Enums;

namespace ResultFunctional.Models.Errors.RestErrors
{
    /// <summary>
    /// Rest error with response message
    /// </summary>
    public class RRestMessageError : RRestError<RRestMessageError>
    {
        /// <summary>
        /// Initialize rest error with response message
        /// </summary>
        /// <param name="httpStatusCode">Rest error type</param>
        /// <param name="reasonPhrase">Reason phrase</param>
        /// <param name="description">Description</param>
        public RRestMessageError(HttpStatusCode httpStatusCode, string reasonPhrase, string description)
            : this(httpStatusCode, reasonPhrase, description, null)
        { }

        /// <summary>
        /// Initialize rest error with response message
        /// </summary>
        /// <param name="httpStatusCode">Rest error type</param>
        /// <param name="reasonPhrase">Reason phrase</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RRestMessageError(HttpStatusCode httpStatusCode, string reasonPhrase, string description, Exception? exception)
            : base(RestErrorType.HttpStatus, description, exception)
        {
            HttpStatusCode = httpStatusCode;
            ReasonPhrase = reasonPhrase;
        }

        /// <summary>
        /// Rest http status code
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// Reason phrase
        /// </summary>
        public string ReasonPhrase { get; }

        /// <summary>
        /// Initialize rest error with response message
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected override RRestMessageError InitializeType(string description, Exception? exception) =>
            new(HttpStatusCode, ReasonPhrase, description, exception);
    }
}