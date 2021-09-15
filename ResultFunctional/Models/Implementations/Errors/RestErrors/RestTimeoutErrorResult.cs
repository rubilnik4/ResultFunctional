using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.RestErrors
{
    /// <summary>
    /// Ошибка сервера. Истекло время ожидания
    /// </summary>
    public class RestTimeoutErrorResult : RestErrorResult<RestTimeoutErrorResult>
    {
        public RestTimeoutErrorResult(string host, TimeSpan timeout, string description)
            : this(host, timeout, description, null)
        { }

        protected RestTimeoutErrorResult(string host, TimeSpan timeout, string description, Exception? exception)
            : base(RestErrorType.RequestTimeout, description, exception)
        {
            Host = host;
            Timeout = timeout;
        }

        /// <summary>
        /// Адрес сервера
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Время ожидания
        /// </summary>
        public TimeSpan Timeout { get; }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override RestTimeoutErrorResult InitializeType(string description, Exception? exception) =>
            new RestTimeoutErrorResult(Host, Timeout, description, exception);
    }
}