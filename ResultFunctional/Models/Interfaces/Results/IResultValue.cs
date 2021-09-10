using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ResultFunctional.Models.Interfaces.Errors.Base;

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
        new IResultValue<TValue> AppendError(IErrorResult error);

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        new IResultValue<TValue> ConcatErrors(IEnumerable<IErrorResult> errors);

        /// <summary>
        /// Преобразовать в результирующую ошибку с типом
        /// </summary>
        IResultValueType<TValue, TError> ToResultValueType<TError>()
            where TError : IErrorResult;
    }
}