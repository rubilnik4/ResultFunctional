using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Interfaces.Results
{
    /// <summary>
    /// Базовый вариант ответа со значением
    /// </summary>
    public interface IResultValue<out TValue>: IResultError
    {
        /// <summary>
        /// Значение
        /// </summary>
        [AllowNull]
        TValue Value { get; }

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        new IResultValue<TValue> ConcatErrors(IEnumerable<IErrorResult> errors);
    }
}