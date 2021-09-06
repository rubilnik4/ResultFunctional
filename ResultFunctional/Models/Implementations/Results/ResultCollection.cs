﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Results
{
    /// <summary>
    /// Базовый вариант ответа с коллекцией
    /// </summary>
    public class ResultCollection<TValue> : ResultValue<IReadOnlyCollection<TValue>>, IResultCollection<TValue>
    {
        public ResultCollection(IErrorResult error)
            : this(error.AsEnumerable()) { }

        public ResultCollection(IEnumerable<IErrorResult> errors)
            : this(Enumerable.Empty<TValue>(), errors)
        { }

        public ResultCollection(IEnumerable<TValue> valueCollection)
            : this(valueCollection, Enumerable.Empty<IErrorResult>())
        { }

        protected ResultCollection([AllowNull] IEnumerable<TValue> valueCollection, IEnumerable<IErrorResult> errors)
            : base(valueCollection!.ToList().AsReadOnly(), errors)
        { }

        /// <summary>
        /// Добавить ошибку
        /// </summary>      
        public new IResultCollection<TValue> ConcatErrors(IEnumerable<IErrorResult> errors) =>
            base.ConcatErrors(errors).ToResultCollection();

        /// <summary>
        /// Преобразовать в результирующий ответ со значением
        /// </summary>
        public IResultValue<IReadOnlyCollection<TValue>> ToResultValue => this;
    }
}