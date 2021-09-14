using System;

namespace ResultFunctional.Models.Interfaces.Errors.Base
{
    /// <summary>
    /// Типизированная ошибка
    /// </summary>
    public interface IErrorBaseExtendResult<out TErrorResult> : IErrorResult
    {
        /// <summary>
        /// Добавить или заменить исключение
        /// </summary>
        new TErrorResult AppendException(Exception exception);
    }
}