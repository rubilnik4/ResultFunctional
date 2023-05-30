using System;
using System.Collections;
using System.Collections.Generic;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.Models.Errors.BaseErrors
{
    /// <summary>
    /// Base error
    /// </summary>
    public abstract class RError : IRError
    {
        /// <summary>
        /// Initialize error
        /// </summary>
        /// <param name="description">Description</param>
        protected RError(string description)
            : this(description, null)
        { }

        /// <summary>
        /// Initialize error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RError(string description, Exception? exception)
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
        protected abstract IRError Initialize(string description, Exception? exception);

        /// <summary>
        /// Is error result type equal to current type
        /// </summary>
        /// <typeparam name="TError">Error result type</typeparam>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        public bool IsError<TError>()
            where TError : IRError =>
            GetType() == typeof(TError);

        /// <summary>
        /// Is error result type equal to current or base type
        /// </summary>
        /// <typeparam name="TError">Error result type</typeparam>
        /// <returns><see langword="true"/> if error equal to the type or base type; otherwise <see langword="false"/></returns>
        public bool HasError<TError>()
            where TError : IRError =>
            this is TError;

        /// <summary>
        /// Is error type equal to current error type
        /// </summary>
        /// <typeparam name="TRErrorType">Error type</typeparam>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        public abstract bool HasErrorType<TRErrorType>()
            where TRErrorType : struct;

        /// <summary>
        /// Is error type value equal to current error type
        /// </summary>
        /// <typeparam name="TRErrorType">Error type</typeparam>
        /// <param name="errorType">Error type value</param>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        public abstract bool HasErrorType<TRErrorType>(TRErrorType errorType)
            where TRErrorType : struct;

        /// <summary>
        /// Create error with exception and base parameters.
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Base error initialized with exception</returns>
        public IRError AppendException(Exception exception) =>
            Initialize(Description, exception);

        /// <summary>
        /// Converting to result type
        /// </summary>
        /// <returns>Result without specific type</returns>
        public IRUnit ToRUnit() =>
            RUnit.None(this);

        /// <summary>
        /// Converting to result type with value
        /// </summary>
        /// <typeparam name="TValue">Specific result type</typeparam>
        /// <returns>Result with specific type</returns>
        public IRValue<TValue> ToRValue<TValue>() 
            where TValue : notnull =>
            RValue<TValue>.None(this);

        /// <summary>
        /// Converting to result collection type with value
        /// </summary>
        /// <typeparam name="TValue">Specific result type</typeparam>
        /// <returns>Result with specific collection type</returns>
        public IRList<TValue> ToRList<TValue>()
            where TValue : notnull =>
            RList<TValue>.None(this);

        #region IEnumerable Support
        /// <summary>
        /// Реализация перечисления
        /// </summary>       
        public IEnumerator<IRError> GetEnumerator()
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