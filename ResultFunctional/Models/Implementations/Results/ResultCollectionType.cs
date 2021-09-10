using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Results
{
    /// <summary>
    /// Базовый вариант ответа с коллекцией и типом ошибки
    /// </summary>
    public class ResultCollectionType<TValue, TError> : ResultCollection<TValue>, IResultCollectionType<TValue, TError>
        where TError : IErrorResult
    {
        public ResultCollectionType(TError error)
          : this(new List<TError> { error })
        { }

        public ResultCollectionType(IEnumerable<TError> errors)
            : this(Enumerable.Empty<TValue>(), errors)
        { }

        public ResultCollectionType(IEnumerable<IErrorResult> errors)
           : this(Enumerable.Empty<TValue>(), errors)
        { }

        public ResultCollectionType(IEnumerable<TValue> valueCollection)
            : this(valueCollection, Enumerable.Empty<TError>())
        { }

        protected ResultCollectionType([AllowNull] IEnumerable<TValue> valueCollection, IEnumerable<TError> errors)
            : this(valueCollection!.ToList().AsReadOnly(), (IEnumerable<IErrorResult>)errors)
        { }

        protected ResultCollectionType([AllowNull] IEnumerable<TValue> valueCollection, IEnumerable<IErrorResult> errors)
           : base(valueCollection!.ToList().AsReadOnly(), errors)
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
        public IResultCollectionType<TValue, TError> AppendError(TError error) =>
            base.AppendError(error).
                 ToResultCollectionType<TError>();

        /// <summary>
        /// Добавить ошибки
        /// </summary>      
        public IResultCollectionType<TValue, TError> ConcatErrors(IEnumerable<TError> errors) =>
            base.ConcatErrors((IEnumerable<IErrorResult>)errors).
                 ToResultCollectionType<TError>();
    }
}