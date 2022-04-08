using System;
using System.Net.Http;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.RestErrors
{
    /// <summary>
    /// Rest error with response message
    /// </summary>
    public class RestMessageErrorResult : RestErrorResult<RestMessageErrorResult>
    {
        /// <summary>
        /// Initialize rest error with response message
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="reasonPhrase">Reason phrase</param>
        /// <param name="description">Description</param>
        public RestMessageErrorResult(RestErrorType restErrorType, string reasonPhrase, string description)
            : this(restErrorType, reasonPhrase, description, null)
        { }

        /// <summary>
        /// Initialize rest error with response message
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="reasonPhrase">Reason phrase</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RestMessageErrorResult(RestErrorType restErrorType, string reasonPhrase, string description, Exception? exception)
            : base(restErrorType, description, exception)
        {
            ReasonPhrase = reasonPhrase;
        }

        /// <summary>
        /// Reason phrase
        /// </summary>
        public string ReasonPhrase { get; }

        /// <summary>
        /// Initialize rest error with response message
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected override RestMessageErrorResult InitializeType(string description, Exception? exception) =>
            new RestMessageErrorResult(ErrorType, ReasonPhrase, description, exception);
    }
}