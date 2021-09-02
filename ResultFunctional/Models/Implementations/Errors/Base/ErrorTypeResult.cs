using System;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.Base
{
    /// <summary>
    /// Ошибка результирующего ответа
    /// </summary>
    public class ErrorTypeResult<TError> : ErrorBaseResult<TError>
        where TError : struct
    {
        public ErrorTypeResult(TError errorType, string description)
           : this(errorType, description, null)
        { }

        public ErrorTypeResult(TError errorType, string description, Exception? exception)
            : base(errorType, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new ErrorTypeResult<TError>(ErrorType, description, exception);
    }
}