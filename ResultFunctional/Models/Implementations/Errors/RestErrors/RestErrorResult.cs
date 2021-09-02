using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.RestErrors
{
    /// <summary>
    /// Ошибка сервера
    /// </summary>
    public class RestErrorResult : ErrorBaseResult<RestErrorType>
    {
        public RestErrorResult(RestErrorType restErrorType, string host, string description)
            : this(restErrorType, host, description, null)
        { }

        protected RestErrorResult(RestErrorType restErrorType, string host, string description, Exception? exception)
            : base(restErrorType, description, exception)
        {
            Host = host;
        }

        /// <summary>
        /// Адрес сервера
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new RestErrorResult(ErrorType, Host, description, exception);
    }
}