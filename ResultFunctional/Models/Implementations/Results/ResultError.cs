using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Results
{
    /// <summary>
    /// Базовый вариант ответа
    /// </summary>
    public class ResultError : IResultError
    {
        public ResultError()
            : this(Enumerable.Empty<IErrorResult>())
        { }

        public ResultError(IErrorResult error)
           : this(error.AsEnumerable())
        { }

        public ResultError(IEnumerable<IErrorResult> errors)
        {
            Errors = errors.ToList().AsReadOnly();
        }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public IReadOnlyCollection<IErrorResult> Errors { get; }

        /// <summary>
        /// Отсутствие ошибок
        /// </summary>
        public bool OkStatus =>
            !HasErrors;

        /// <summary>
        /// Присутствуют ли ошибки
        /// </summary>
        public bool HasErrors =>
            Errors.Any();

        /// <summary>
        /// Получить типы ошибок
        /// </summary>
        public IReadOnlyCollection<Type> GetErrorTypes() =>
            Errors.
            Select(error => error.GetType()).
            ToList();

        /// <summary>
        /// Присутствует ли тип ошибки
        /// </summary>
        public bool HasErrorType<TError>()
            where TError : struct =>
            Errors.Any(error => error.HasErrorType<TError>());

        /// <summary>
        /// Получить ошибку
        /// </summary>      
        public IErrorBaseResult<TError>? GetError<TError>()
            where TError : struct =>
            Errors.
            OfType<IErrorBaseResult<TError>>().
            FirstOrDefault();

        /// <summary>
        /// Получить ошибку
        /// </summary>      
        public IReadOnlyCollection<IErrorBaseResult<TError>> GetErrors<TError>()
            where TError : struct =>
            Errors.
            OfType<IErrorBaseResult<TError>>().
            ToList();

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        public IResultError AppendError<TError>(IErrorResult error)
            where TError : struct =>
            new ResultError(Errors.Append(error));

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        public IResultError ConcatErrors(IEnumerable<IErrorResult> errors) =>
            new ResultError(Errors.Union(errors));
    }
}