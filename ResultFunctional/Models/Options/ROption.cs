using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Options;

internal abstract class ROption<TValue, TOption> : IROption<TValue>
    where TValue : notnull
    where TOption : IROption<TValue>
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

    public TValue? Value { get; }

    public IReadOnlyCollection<IRError> Errors { get; } = new List<IRError>();

    /// <summary>
    /// hasn't errors
    /// </summary>
    public bool IsSuccess =>
        !HasErrors;

    /// <summary>
    /// Has error
    /// </summary>
    public bool HasErrors =>
        Errors.Any();

    protected abstract TOption Initialize(IReadOnlyCollection<IRError> errors);
    
    public TValue GetValue() =>
        Value ?? throw new ArgumentNullException(nameof(Value));

    public IReadOnlyCollection<IRError> GetErrors() =>
        Errors ?? throw new ArgumentNullException(nameof(Errors));

    /// <summary>
    /// Is error result type equal to current type
    /// </summary>
    /// <typeparam name="TError">Error result type</typeparam>
    /// <returns><see langword="true"/> if error equal to the current type; otherwise <see langword="false"/></returns>
    public bool IsError<TError>()
        where TError : IRError =>
        Errors.Any(error => error.IsError<TError>());

    /// <summary>
    /// Is error result type equal to current or base type
    /// </summary>
    /// <typeparam name="TError">Error result type</typeparam>
    /// <returns><see langword="true"/> if error equal or derived to the current type; otherwise <see langword="false"/></returns>
    public bool HasError<TError>()
        where TError : IRError =>
        Errors.Any(error => error.HasError<TError>());

    /// <summary>
    /// Get type of errors
    /// </summary>
    /// <returns>Errors types</returns>
    public IReadOnlyCollection<Type> GetErrorByTypes() =>
        Errors.Select(error => error.GetType()).ToList();

    /// <summary>
    /// Is error type equal to current or base error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <returns><see langword="true"/> if error type equal to the current error type; otherwise <see langword="false"/></returns>
    public bool HasErrorType<TErrorType>()
        where TErrorType : struct =>
        Errors.Any(error => error is IRBaseError<TErrorType>);

    /// <summary>
    /// Is error type equal to current or base error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <param name="errorType">Error type</param>
    /// <returns><see langword="true"/> if error type equal to the current error type; otherwise <see langword="false"/></returns>
    public bool HasErrorType<TErrorType>(TErrorType errorType)
        where TErrorType : struct =>
        Errors.OfType<IRBaseError<TErrorType>>()
              .Any(error => error.ErrorType.Equals(errorType));

    /// <summary>
    /// Get error filtered by error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <returns>Base error result filtered by error type</returns>
    public IRBaseError<TErrorType>? GetErrorByType<TErrorType>()
        where TErrorType : struct =>
        Errors.OfType<IRBaseError<TErrorType>>().FirstOrDefault();

    /// <summary>
    /// Get errors filtered by error type
    /// </summary>
    /// <typeparam name="TErrorType">Error type</typeparam>
    /// <returns>Base errors result filtered by error type</returns>
    public IReadOnlyCollection<IRBaseError<TErrorType>> GetErrorsByTypes<TErrorType>()
        where TErrorType : struct =>
        Errors.OfType<IRBaseError<TErrorType>>().ToList();

    /// <summary>
    /// Add error to result
    /// </summary>
    /// <param name="error">Error</param>
    /// <returns>Result with error</returns>    
    public TOption AppendError(IRError error) =>
        Initialize(Errors.Concat(error).ToList());

    /// <summary>
    /// Add errors to result
    /// </summary>
    /// <param name="errors">Errors</param>
    /// <returns>Result with error</returns>  
    public TOption ConcatErrors(IEnumerable<IRError> errors) =>
        Initialize(Errors.Concat(errors).ToList());

    /// <summary>
    /// Add values and errors to current result
    /// </summary>
    /// <param name="option">Result error</param>
    /// <returns>Result error</returns>
    public TOption ConcatErrors(IROption<TValue> option) =>
        Initialize(Errors.Concat(option.Errors).ToList());
}