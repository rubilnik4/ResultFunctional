using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Options;

/// <summary>
/// Base result with value
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
/// <typeparam name="TOption">Result option</typeparam>
internal abstract class ROption<TValue, TOption> : IROption<TValue, TOption>
    where TValue : notnull
    where TOption : IROption<TValue, TOption>
{
    protected ROption(TValue value)
    {
        Value = value;
    }

    protected ROption(IRError error)
        : this(error.ToList())
    { }

    protected ROption(IReadOnlyCollection<IRError> errors)
    {
        Errors = errors;
    }

    /// <summary>
    /// Value
    /// </summary>
    public TValue? Value { get; }

    /// <summary>
    /// Errors
    /// </summary>
    public IReadOnlyCollection<IRError>? Errors { get; }

    /// <summary>
    /// Has value
    /// </summary>
    public bool Success =>
        !Failure;

    /// <summary>
    /// Has errors
    /// </summary>
    public bool Failure =>
        Errors?.Any() == true;

    /// <summary>
    /// Initialize result by errors
    /// </summary>
    /// <param name="errors">Errors</param>
    /// <returns>Result option</returns>
    protected abstract TOption Initialize(IReadOnlyCollection<IRError> errors);

    /// <summary>
    /// Get value
    /// </summary>
    /// <returns>Value</returns>
    /// <exception cref="ArgumentNullException">Throw exception if value not found</exception>
    public TValue GetValue() =>
        Value ?? throw new ArgumentNullException(nameof(Value));

    /// <summary>
    /// Get errors
    /// </summary>
    /// <returns>Errors</returns>
    /// <exception cref="ArgumentNullException">Throw exception if errors not found</exception>
    public IReadOnlyCollection<IRError> GetErrors() =>
        Errors ?? throw new ArgumentNullException(nameof(Errors));

    /// <summary>
    /// Get errors or empty list
    /// </summary>
    /// <returns>Errors</returns>
    public IReadOnlyCollection<IRError> GetErrorsOrEmpty() =>
        Errors ?? new List<IRError>();

    /// <summary>
    /// Is error type equal to current type
    /// </summary>
    /// <typeparam name="TError">Error type</typeparam>
    /// <returns><see langword="true"/> if error equal to the current type; otherwise <see langword="false"/></returns>
    public bool IsError<TError>()
        where TError : IRError =>
        Errors?.Any(error => error.IsError<TError>()) == true;

    /// <summary>
    /// Is error type equal to current or base type
    /// </summary>
    /// <typeparam name="TError">Error type</typeparam>
    /// <returns><see langword="true"/> if error equal or derived to the current type; otherwise <see langword="false"/></returns>
    public bool HasError<TError>()
        where TError : IRError =>
        Errors?.Any(error => error.HasError<TError>()) == true;

    /// <summary>
    /// Get type of errors
    /// </summary>
    /// <returns>Errors types</returns>
    public IReadOnlyCollection<Type> GetErrorTypes() =>
        Errors?.Select(error => error.GetType())
               .Distinct()
               .ToList() 
        ?? new List<Type>();

    /// <summary>
    /// Is error type equal to current or base error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <returns><see langword="true"/> if error type equal to the current error type; otherwise <see langword="false"/></returns>
    public bool HasErrorType<TErrorType>()
        where TErrorType : struct =>
        Errors?.Any(error => error is IRBaseError<TErrorType>) == true;

    /// <summary>
    /// Is error type equal to current or base error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <param name="errorType">Error type</param>
    /// <returns><see langword="true"/> if error type equal to the current error type; otherwise <see langword="false"/></returns>
    public bool HasErrorType<TErrorType>(TErrorType errorType)
        where TErrorType : struct =>
        Errors?.OfType<IRBaseError<TErrorType>>()
               .Any(error => error.ErrorType.Equals(errorType)) == true;

    /// <summary>
    /// Get error filtered by error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <returns>Base error result filtered by error type</returns>
    public IRBaseError<TErrorType>? GetErrorByType<TErrorType>()
        where TErrorType : struct =>
        Errors?.OfType<IRBaseError<TErrorType>>().FirstOrDefault();

    /// <summary>
    /// Get errors filtered by error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <returns>Base errors filtered by error type</returns>
    public IReadOnlyCollection<IRBaseError<TErrorType>> GetErrorsByTypes<TErrorType>()
        where TErrorType : struct =>
        Errors?.OfType<IRBaseError<TErrorType>>().ToList() ?? new List<IRBaseError<TErrorType>>();

    /// <summary>
    /// Add error to result
    /// </summary>
    /// <param name="error">Error</param>
    /// <returns>Result with error</returns>    
    public TOption AppendError(IRError error) =>
        GetErrorsOrEmpty().Concat(error).ToList()
            .Map(Initialize);

    /// <summary>
    /// Add errors to result
    /// </summary>
    /// <param name="errors">Errors</param>
    /// <returns>Result option with errors</returns>  
    public TOption ConcatErrors(IEnumerable<IRError> errors) =>
        GetErrorsOrEmpty().Concat(errors).ToList()
            .Map(Initialize);
}