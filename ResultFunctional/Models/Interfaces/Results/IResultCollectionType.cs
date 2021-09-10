using System.Collections.Generic;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Results
{
    /// <summary>
    /// Базовый вариант ответа с коллекцией с типом ошибки
    /// </summary>
    public interface IResultCollectionType<out TValue, TError> : IResultCollection<TValue>, IResultErrorType<TError>
        where TError : IErrorResult
    {
        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        new IResultCollectionType<TValue, TError> AppendError(TError error);

        /// <summary>
        /// Добавить ошибки
        /// </summary>      
        new IResultCollectionType<TValue, TError> ConcatErrors(IEnumerable<TError> errors);
    }
}