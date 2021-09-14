using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.RestErrors
{
    /// <summary>
    /// Ошибка сервера
    /// </summary>
    public class RestHostErrorResult : RestErrorResult<RestHostErrorResult>
    {
        public RestHostErrorResult(RestErrorType restErrorType, string host, string description)
            : this(restErrorType, host, description, null)
        { }

        protected RestHostErrorResult(RestErrorType restErrorType, string host, string description, Exception? exception)
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
        protected override RestHostErrorResult InitializeType(string description, Exception? exception) =>
            new RestHostErrorResult(ErrorType, Host, description, exception);
    }
}