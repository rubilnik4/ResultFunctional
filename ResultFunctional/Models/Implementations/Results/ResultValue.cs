using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Results
{
    /// <summary>
    /// Базовый вариант ответа со значением
    /// </summary>
    public class ResultValue<TValue> : ResultError, IResultValue<TValue>
    {
        public ResultValue(IErrorResult error)
            : this(error.AsEnumerable()) { }

        public ResultValue(IEnumerable<IErrorResult> errors)
            : this(default, errors)
        { }

        public ResultValue(TValue value)
            : this(value, Enumerable.Empty<IErrorResult>())
        { }

        protected ResultValue([AllowNull] TValue value, IEnumerable<IErrorResult> errors)
            : base(errors)
        {
            if (value == null && !Errors.Any()) throw new ArgumentNullException(nameof(errors));
            Value = value;
        }

        /// <summary>
        /// Значение
        /// </summary>
        [AllowNull]
        public TValue Value { get; }

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        public new IResultValue<TValue> AppendError(IErrorResult error) =>
            base.AppendError(error).
            ToResultValue(Value);

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        public new IResultValue<TValue> ConcatErrors(IEnumerable<IErrorResult> errors) =>
            base.ConcatErrors(errors).
            ToResultValue(Value);

        /// <summary>
        /// Добавить ошибки из результирующего ответа
        /// </summary>      
        public new IResultValue<TValue> ConcatResult(IResultError result) =>
            ConcatErrors(result.Errors);
    }
}