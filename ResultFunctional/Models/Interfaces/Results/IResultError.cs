using System;
using System.Collections.Generic;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Results
{
    /// <summary>
    /// Базовый вариант ответа
    /// </summary>
    public interface IResultError
    {
        /// <summary>
        /// Список ошибок
        /// </summary>
        IReadOnlyCollection<IErrorResult> Errors { get; }

        /// <summary>
        /// Отсутствие ошибок
        /// </summary>
        bool OkStatus { get; }

        /// <summary>
        /// Присутствуют ли ошибки
        /// </summary>
        bool HasErrors { get; }

        /// <summary>
        /// Получить типы ошибок
        /// </summary>
        IReadOnlyCollection<Type> GetErrorTypes();

        /// <summary>
        /// Присутствует ли тип ошибки
        /// </summary>
        bool HasErrorType<TError>()
            where TError : struct;

        /// <summary>
        /// Получить ошибку
        /// </summary>      
        IErrorBaseResult<TError>? GetError<TError>()
            where TError : struct;

        /// <summary>
        /// Получить ошибку
        /// </summary>      
        IReadOnlyCollection<IErrorBaseResult<TError>> GetErrors<TError>()
            where TError : struct;

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        IResultError AppendError<TError>(IErrorResult error)
            where TError : struct;

        /// <summary>
        /// Добавить ошибки
        /// </summary>      
        IResultError ConcatErrors(IEnumerable<IErrorResult> errors);
    }
}