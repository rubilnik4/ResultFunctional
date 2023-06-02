using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.Models.Maybe;

/// <summary>
/// Base result
/// </summary>
public interface IRMaybe
{
    /// <summary>
    /// Errors
    /// </summary>
    IReadOnlyCollection<IRError>? Errors { get; }

    /// <summary>
    /// Has value
    /// </summary>
    bool Success { get; }

    /// <summary>
    /// Has errors
    /// </summary>
    bool Failure { get; }

    /// <summary>
    /// Get errors
    /// </summary>
    /// <returns>Errors</returns>
    /// <exception cref="ArgumentNullException">Throw exception if errors not found</exception>
    IReadOnlyCollection<IRError> GetErrors();

    /// <summary>
    /// Get errors or empty list
    /// </summary>
    /// <returns>Errors</returns>
    IReadOnlyCollection<IRError> GetErrorsOrEmpty();

    /// <summary>
    /// Is error type equal to current type
    /// </summary>
    /// <typeparam name="TError">Error type</typeparam>
    /// <returns><see langword="true"/> if error equal to the current type; otherwise <see langword="false"/></returns>
    bool IsAnyError<TError>()
        where TError : IRError;

    /// <summary>
    /// Is error type equal to current or base type
    /// </summary>
    /// <typeparam name="TError">Error type</typeparam>
    /// <returns><see langword="true"/> if error equal or derived to the current type; otherwise <see langword="false"/></returns>
    bool HasAnyError<TError>()
        where TError : IRError;

    /// <summary>
    /// Get type of errors
    /// </summary>
    /// <returns>Errors types</returns>
    IReadOnlyCollection<Type> GetErrorTypes();

    /// <summary>
    /// Is error type equal to current or base error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <returns><see langword="true"/> if error type equal to the current error type; otherwise <see langword="false"/></returns>
    bool HasAnyErrorType<TErrorType>()
        where TErrorType : struct;

    /// <summary>
    /// Is error type equal to current or base error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <param name="errorType">Error type</param>
    /// <returns><see langword="true"/> if error type equal to the current error type; otherwise <see langword="false"/></returns>
    bool HasAnyErrorType<TErrorType>(TErrorType errorType)
        where TErrorType : struct;

    /// <summary>
    /// Get error filtered by error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <returns>Base error result filtered by error type</returns>
    IRBaseError<TErrorType>? GetErrorByType<TErrorType>()
        where TErrorType : struct;

    /// <summary>
    /// Get errors filtered by error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <returns>Base errors filtered by error type</returns>
    IReadOnlyCollection<IRBaseError<TErrorType>> GetErrorsByTypes<TErrorType>()
        where TErrorType : struct;

    /// <summary>
    /// Converting to result unit
    /// </summary>
    /// <returns>Result unit</returns>
    IRUnit ToRUnit();

    /// <summary>
    /// Converting to result value
    /// </summary>
    /// <typeparam name="TValue">Value</typeparam>
    /// <returns>Result value</returns>
    IRValue<TValue> ToRValue<TValue>(TValue value) 
        where TValue : notnull;

    /// <summary>
    /// Converting to result collection
    /// </summary>
    /// <typeparam name="TValue">Value</typeparam>
    /// <returns>Result collection</returns>
    IRList<TValue> ToRList<TValue>(IReadOnlyCollection<TValue> values)
        where TValue : notnull;
}