﻿using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Results
{
    /// <summary>
    /// Result error without value
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
        /// Присутствует(является) ли тип ошибки
        /// </summary>
        public bool IsError<TError>()
            where TError : IErrorResult =>
            Errors.Any(error => error.IsErrorResult<TError>());

        /// <summary>
        /// Присутствует(включен) ли тип ошибки
        /// </summary>
        public bool HasError<TError>()
            where TError : IErrorResult =>
            Errors.Any(error => error.HasErrorResult<TError>());

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
        public bool HasErrorType<TErrorType>()
            where TErrorType : struct =>
            Errors.Any(error => error is IErrorBaseResult<TErrorType>);

        /// <summary>
        /// Присутствует ли тип ошибки
        /// </summary>
        public bool HasErrorType<TErrorType>(TErrorType errorType)
            where TErrorType : struct =>
            Errors.OfType<IErrorBaseResult<TErrorType>>().
                   Any(error => error.ErrorType.Equals(errorType));

        /// <summary>
        /// Получить ошибку
        /// </summary>      
        public IErrorBaseResult<TErrorType>? GetErrorType<TErrorType>()
            where TErrorType : struct =>
            Errors.
            OfType<IErrorBaseResult<TErrorType>>().
            FirstOrDefault();

        /// <summary>
        /// Получить ошибку
        /// </summary>      
        public IReadOnlyCollection<IErrorBaseResult<TErrorType>> GetErrorTypes<TErrorType>()
            where TErrorType : struct =>
            Errors.
            OfType<IErrorBaseResult<TErrorType>>().
            ToList();

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        public IResultError AppendError(IErrorResult error) =>
            new ResultError(Errors.Concat(error));

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        public IResultError ConcatErrors(IEnumerable<IErrorResult> errors) =>
            new ResultError(Errors.Concat(errors));

        /// <summary>
        /// Добавить ошибки из результирующего ответа
        /// </summary>      
        public IResultError ConcatResult(IResultError result) =>
            new ResultError(Errors.Concat(result.Errors));
    }
}