using System;
using System.Collections.Generic;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Interfaces.Errors.Base
{
    /// <summary>
    /// Base error
    /// </summary>
    public interface IErrorResult : IEnumerable<IErrorResult>
    {
        /// <summary>
        /// Error ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Exception
        /// </summary>
        Exception? Exception { get; }

        /// <summary>
        /// Is error result type equal to current type
        /// </summary>
        /// <typeparam name="TErrorType">Error result type</typeparam>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        bool IsErrorResult<TErrorType>()
            where TErrorType : IErrorResult;

        /// <summary>
        /// Is error result type equal to current or base type
        /// </summary>
        /// <typeparam name="TErrorType">Error result type</typeparam>
        /// <returns><see langword="true"/> if error equal to the type or base type; otherwise <see langword="false"/></returns>
        bool HasErrorResult<TErrorType>()
            where TErrorType : IErrorResult;

        /// <summary>
        /// Is error type equal to current error type
        /// </summary>
        /// <typeparam name="TErrorTypeCompare">Error type</typeparam>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        bool IsErrorType<TErrorTypeCompare>()
            where TErrorTypeCompare : struct;

        /// <summary>
        /// Is error type value equal to current error type
        /// </summary>
        /// <typeparam name="TErrorTypeCompare">Error type</typeparam>
        /// <param name="errorType">Error type value</param>
        /// <returns><see langword="true"/> if error equal to the type; otherwise <see langword="false"/></returns>
        bool IsErrorType<TErrorTypeCompare>(TErrorTypeCompare errorType)
            where TErrorTypeCompare : struct;

        /// <summary>
        /// Create error  with exception and base parameters.
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Base error initialized with exception</returns>
        IErrorResult AppendException(Exception exception);

        /// <summary>
        /// Converting to result type
        /// </summary>
        /// <returns>Result without specific type</returns>
        IResultError ToResult();

        /// <summary>
        /// Converting to result type with value
        /// </summary>
        /// <typeparam name="TValue">Specific result type</typeparam>
        /// <returns>Result with specific type</returns>
        IResultValue<TValue> ToResultValue<TValue>();

        /// <summary>
        /// Converting to result collection type with value
        /// </summary>
        /// <typeparam name="TValue">Specific result type</typeparam>
        /// <returns>Result with specific collection type</returns>
        IResultCollection<TValue> ToResultCollection<TValue>();
    }
}