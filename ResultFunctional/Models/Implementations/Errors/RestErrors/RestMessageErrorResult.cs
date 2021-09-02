using System;
using System.Net.Http;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.RestErrors
{
    /// <summary>
    /// Ошибка сервера с сообщением
    /// </summary>
    public class RestMessageErrorResult : ErrorBaseResult<RestErrorType>
    {
        public RestMessageErrorResult(RestErrorType restErrorType, HttpResponseMessage httpResponseMessage, string description)
            : this(restErrorType, httpResponseMessage, description, null)
        { }

        protected RestMessageErrorResult(RestErrorType restErrorType, HttpResponseMessage httpResponseMessage, string description, Exception? exception)
            : base(restErrorType, description, exception)
        {
            HttpResponseMessage = httpResponseMessage;
        }

        /// <summary>
        /// Адрес сервера
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new RestMessageErrorResult(ErrorType, HttpResponseMessage, description, exception);
    }
}