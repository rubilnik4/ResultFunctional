﻿using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Results
{
    /// <summary>
    /// Базовый вариант ответа с типом ошибки
    /// </summary>
    public interface IResultErrorType<TError> : IResultError
        where TError : IErrorResult
    {
        /// <summary>
        /// Список ошибок текущего типа
        /// </summary>
        IReadOnlyCollection<TError> ErrorsByType { get; }

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        IResultErrorType<TError> AppendError(TError error) =>
            Errors.Append(error).
            Map(errors => new ResultErrorType<TError>(errors));

        /// <summary>
        /// Добавить ошибки
        /// </summary>      
        IResultErrorType<TError> ConcatErrors(IEnumerable<TError> errors) =>
            Errors.Concat((IEnumerable<IErrorResult>)errors).
            Map(errorsConcat => new ResultErrorType<TError>(errorsConcat));
    }
}