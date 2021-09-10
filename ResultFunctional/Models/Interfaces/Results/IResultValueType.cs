using System.Collections.Generic;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Results
{
    /// <summary>
    /// Базовый вариант ответа со значением и типом ошибки
    /// </summary>
    public interface IResultValueType<out TValue, TError> : IResultValue<TValue>, IResultErrorType<TError>
        where TError : IErrorResult
    {
        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        new IResultValueType<TValue, TError> AppendError(TError error);

        /// <summary>
        /// Добавить ошибки
        /// </summary>      
        new IResultValueType<TValue, TError> ConcatErrors(IEnumerable<TError> errors);
    }
}