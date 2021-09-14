using System;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.Base
{
    /// <summary>
    /// Ошибка результирующего ответа с типизированной ошибкой
    /// </summary>
    public class ErrorTypeResult<TError> : ErrorBaseResult<TError, ErrorTypeResult<TError>>
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
        protected override ErrorTypeResult<TError> InitializeType(string description, Exception? exception) =>
            new ErrorTypeResult<TError>(ErrorType, description, exception);
    }
}