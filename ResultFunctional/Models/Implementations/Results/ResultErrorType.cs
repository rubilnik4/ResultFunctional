using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Results
{
    /// <summary>
    /// Базовый вариант ответа с типом ошибки
    /// </summary>
    public class ResultErrorType<TError> : ResultError, IResultErrorType<TError>
        where TError : IErrorResult
    {
        public ResultErrorType()
            : this(Enumerable.Empty<TError>())
        { }

        public ResultErrorType(TError error)
           : this(new List<TError> { error })
        { }

        public ResultErrorType(IEnumerable<TError> errors)
            : this((IEnumerable<IErrorResult>)errors)
        { }

        public ResultErrorType(IEnumerable<IErrorResult> errors)
           : base(errors)
        { }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public IReadOnlyCollection<TError> ErrorsByType =>
            Errors.OfType<TError>().
            ToList();

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        public IResultErrorType<TError> AppendError(TError error) =>
            Errors.Append(error).
            Map(errors => new ResultErrorType<TError>(errors));

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        public IResultErrorType<TError> ConcatErrors(IEnumerable<TError> errors) =>
            Errors.Concat((IEnumerable<IErrorResult>)errors).
            Map(errorsConcat => new ResultErrorType<TError>(errorsConcat));
    }
}