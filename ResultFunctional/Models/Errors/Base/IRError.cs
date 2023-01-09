using System;
using System.Collections.Generic;
using ResultFunctional.Models.Interfaces.Results;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.Models.Errors.Base
{
    /// <summary>
    /// Base error
    /// </summary>
    public interface IRError : IEnumerable<IRError>
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
        bool IsError<TErrorType>()
            where TErrorType : IRError;

        /// <summary>
        /// Is error result type equal to current or base type
        /// </summary>
        /// <typeparam name="TErrorType">Error result type</typeparam>
        /// <returns><see langword="true"/> if error equal to the type or base type; otherwise <see langword="false"/></returns>
        bool HasError<TErrorType>()
            where TErrorType : IRError;

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
        IRError AppendException(Exception exception);

        /// <summary>
        /// Converting to result type
        /// </summary>
        /// <returns>Result without specific type</returns>
        IRUnit ToRUnit();

        /// <summary>
        /// Converting to result type with value
        /// </summary>
        /// <typeparam name="TValue">Specific result type</typeparam>
        /// <returns>Result with specific type</returns>
        IRValue<TValue> ToResultValue<TValue>()
            where TValue : notnull;

        /// <summary>
        /// Converting to result collection type with value
        /// </summary>
        /// <typeparam name="TValue">Specific result type</typeparam>
        /// <returns>Result with specific collection type</returns>
        IRList<TValue> ToResultCollection<TValue>()
            where TValue : notnull;
    }
}