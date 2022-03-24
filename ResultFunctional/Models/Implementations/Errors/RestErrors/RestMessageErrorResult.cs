using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.RestErrors
{
    /// <summary>
    /// Ошибка сервера с сообщением
    /// </summary>
    public class RestMessageErrorResult : RestErrorResult<RestMessageErrorResult>
    {
        public RestMessageErrorResult(RestErrorType restErrorType, string reasonPhrase, string description)
            : this(restErrorType, reasonPhrase, description, null)
        { }

        protected RestMessageErrorResult(RestErrorType restErrorType, string reasonPhrase, string description, Exception? exception)
            : base(restErrorType, description, exception)
        {
            ReasonPhrase = reasonPhrase;
        }

        /// <summary>
        /// Адрес сервера
        /// </summary>
        public string ReasonPhrase { get; }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override RestMessageErrorResult InitializeType(string description, Exception? exception) =>
            new RestMessageErrorResult(ErrorType, ReasonPhrase, description, exception);
    }
}