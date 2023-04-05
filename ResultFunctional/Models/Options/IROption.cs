using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.Models.Options;

/// <summary>
/// Base result with value
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
/// <typeparam name="TOption">Base result</typeparam>
public interface IROption<out TValue, out TOption>
    where TValue : notnull
    where TOption : IROption<TValue, TOption>
{
    /// <summary>
    /// Value
    /// </summary>
    TValue? Value { get; }

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
    /// Get value
    /// </summary>
    /// <returns>Value</returns>
    /// <exception cref="ArgumentNullException">Throw exception if value not found</exception>
    TValue GetValue();

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
    bool IsError<TError>() 
        where TError : IRError;

    /// <summary>
    /// Is error type equal to current or base type
    /// </summary>
    /// <typeparam name="TError">Error type</typeparam>
    /// <returns><see langword="true"/> if error equal or derived to the current type; otherwise <see langword="false"/></returns>
    bool HasError<TError>() 
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
    bool HasErrorType<TErrorType>() 
        where TErrorType : struct;

    /// <summary>
    /// Is error type equal to current or base error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <param name="errorType">Error type</param>
    /// <returns><see langword="true"/> if error type equal to the current error type; otherwise <see langword="false"/></returns>
    bool HasErrorType<TErrorType>(TErrorType errorType)
        where TErrorType : struct;

    /// <summary>
    /// Get error filtered by error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <returns>Base error result filtered by error type</returns>
    IRBaseError<TErrorType>? GetErrorByType<TErrorType>()
        where TErrorType : struct ;

    /// <summary>
    /// Get errors filtered by error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <returns>Base errors filtered by error type</returns>
    IReadOnlyCollection<IRBaseError<TErrorType>> GetErrorsByTypes<TErrorType>()
        where TErrorType : struct ;

    /// <summary>
    /// Add error to result
    /// </summary>
    /// <param name="error">Error</param>
    /// <returns>Result with error</returns>    
    TOption AppendError(IRError error);

    /// <summary>
    /// Add errors to result
    /// </summary>
    /// <param name="errors">Errors</param>
    /// <returns>Result option with errors</returns>  
    TOption ConcatErrors(IEnumerable<IRError> errors);
}