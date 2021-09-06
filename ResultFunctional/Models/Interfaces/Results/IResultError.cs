﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Присутствует(является) ли тип ошибки
        /// </summary>
        public bool HasError<TError>()
            where TError : IErrorResult =>
            Errors.Any(error => error.GetType() == typeof(TError));

        /// <summary>
        /// Присутствует(включен) ли тип ошибки
        /// </summary>
        public bool FromError<TError>()
            where TError : IErrorResult =>
            Errors.Any(error => error is TError);

        /// <summary>
        /// Получить типы ошибок
        /// </summary>
        IReadOnlyCollection<Type> GetErrorTypes();

        /// <summary>
        /// Присутствует ли тип ошибки
        /// </summary>
        bool HasErrorType<TErrorType>()
            where TErrorType : struct;

        /// <summary>
        /// Получить ошибку
        /// </summary>      
        IErrorBaseResult<TErrorType>? GetErrorType<TErrorType>()
            where TErrorType : struct;

        /// <summary>
        /// Получить ошибку
        /// </summary>      
        IReadOnlyCollection<IErrorBaseResult<TErrorType>> GetErrorTypes<TErrorType>()
            where TErrorType : struct;

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        IResultError AppendError<TErrorType>(IErrorResult error)
            where TErrorType : struct;

        /// <summary>
        /// Добавить ошибки
        /// </summary>      
        IResultError ConcatErrors(IEnumerable<IErrorResult> errors);
    }
}