using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Results
{
    /// <summary>
    /// Базовый вариант ответа со значением и типом ошибки
    /// </summary>
    public class ResultValueType<TValue, TError> : ResultValue<TValue>, IResultValueType<TValue, TError>
        where TError : IErrorResult
    {
        public ResultValueType(TError error)
          : this(new List<TError> { error })
        { }

        public ResultValueType(IEnumerable<TError> errors)
            : this(default, errors)
        { }

        public ResultValueType(IEnumerable<IErrorResult> errors)
           : this(default, errors)
        { }

        public ResultValueType(TValue value)
            : this(value, Enumerable.Empty<TError>())
        { }

        protected ResultValueType([AllowNull] TValue value, IEnumerable<TError> errors)
            : this(value, (IEnumerable<IErrorResult>)errors)
        { }

        protected ResultValueType([AllowNull] TValue value, IEnumerable<IErrorResult> errors)
            : base(value, errors)
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
        public IResultValueType<TValue, TError> AppendError(TError error) =>
            base.AppendError(error).
                 ToResultValueType<TError>();

        /// <summary>
        /// Добавить ошибки
        /// </summary>      
        public IResultValueType<TValue, TError> ConcatErrors(IEnumerable<TError> errors) =>
            base.ConcatErrors((IEnumerable<IErrorResult>)errors).
                 ToResultValueType<TError>();
    }
}