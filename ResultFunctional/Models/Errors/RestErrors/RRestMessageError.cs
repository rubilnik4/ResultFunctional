using System;
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
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="reasonPhrase">Reason phrase</param>
        /// <param name="description">Description</param>
        public RRestMessageError(RestErrorType restErrorType, string reasonPhrase, string description)
            : this(restErrorType, reasonPhrase, description, null)
        { }

        /// <summary>
        /// Initialize rest error with response message
        /// </summary>
        /// <param name="restErrorType">Rest error type</param>
        /// <param name="reasonPhrase">Reason phrase</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RRestMessageError(RestErrorType restErrorType, string reasonPhrase, string description, Exception? exception)
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
        protected override RRestMessageError InitializeType(string description, Exception? exception) =>
            new(ErrorType, ReasonPhrase, description, exception);
    }
}