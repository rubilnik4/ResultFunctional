using System.Collections.Generic;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Results
{
    /// <summary>
    /// Базовый вариант ответа с коллекцией
    /// </summary>
    public interface IResultCollection<out TValue> : IResultValue<IReadOnlyCollection<TValue>>
    {
        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        new IResultCollection<TValue> AppendError(IErrorResult error);

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        new IResultCollection<TValue> ConcatErrors(IEnumerable<IErrorResult> errors);

        /// <summary>
        /// Преобразовать в результирующий ответ со значением
        /// </summary>
        IResultValue<IReadOnlyCollection<TValue>> ToResultValue();
    }
}