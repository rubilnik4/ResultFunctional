using System;

namespace ResultFunctional.Models.Interfaces.Errors.Base
{
    /// <summary>
    /// Ошибка результирующего ответа
    /// </summary>
    public interface IErrorBaseResult<out TError> : IErrorResult, IFormattable
        where TError : struct
    {
        /// <summary>
        /// Тип ошибки при конвертации файлов
        /// </summary>
        TError ErrorType { get; }
    }
}