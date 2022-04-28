using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.Base
{
    /// <summary>
    /// Base error
    /// </summary>
    public abstract class ErrorResult : IErrorResult
    {
        /// <summary>
        /// Initialize error
        /// </summary>
        /// <param name="description">Description</param>
        protected ErrorResult(string description)
            : this(description, null)
        { }

        /// <summary>
        /// Initialize error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected ErrorResult(string description, Exception? exception)
        {
            Description = description;
            Exception = exception;
        }

        /// <summary>
        /// Error type ID
        /// </summary>
        public abstract string Id { get; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Exception
        /// </summary>
        public Exception? Exception { get; }

        /// <summary>
        /// Initialize base error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Base error initialized by derived</returns>
        protected abstract IErrorResult Initialize(string description, Exception? exception);

        /// <summary>
        /// Is error result type equal to current type
        /// </summary>
        /// <typeparam name="TErrorResult">Error result type</typeparam>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        public bool IsErrorResult<TErrorResult>()
            where TErrorResult : IErrorResult =>
            GetType() == typeof(TErrorResult);

        /// <summary>
        /// Is error result type equal to current or base type
        /// </summary>
        /// <typeparam name="TErrorResult">Error result type</typeparam>
        /// <returns><see langword="true"/> if error equal to the type or base type; otherwise <see langword="false"/></returns>
        public bool HasErrorResult<TErrorResult>()
            where TErrorResult : IErrorResult =>
            this is TErrorResult;

        /// <summary>
        /// Create error with exception and base parameters.
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Base error initialized with exception</returns>
        public IErrorResult AppendException(Exception exception) =>
            Initialize(Description, exception);

        /// <summary>
        /// Converting to result type
        /// </summary>
        /// <returns>Result without specific type</returns>
        public IResultError ToResult() =>
            new ResultError(this);

        /// <summary>
        /// Converting to result type with value
        /// </summary>
        /// <typeparam name="TValue">Specific result type</typeparam>
        /// <returns>Result with specific type</returns>
        public IResultValue<TValue> ToResultValue<TValue>() =>
            new ResultValue<TValue>(this);

        /// <summary>
        /// Converting to result collection type with value
        /// </summary>
        /// <typeparam name="TValue">Specific result type</typeparam>
        /// <returns>Result with specific collection type</returns>
        public IResultCollection<TValue> ToResultCollection<TValue>() =>
            new ResultCollection<TValue>(this);

        #region IEnumerable Support
        /// <summary>
        /// Реализация перечисления
        /// </summary>       
        public IEnumerator<IErrorResult> GetEnumerator()
        {
            yield return this;
        }

        /// <summary>
        /// Реализация перечисления
        /// </summary>  
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
        #endregion
    }
}