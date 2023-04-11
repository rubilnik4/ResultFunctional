using System.Collections.Generic;
using System;
using System.Linq;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.Models.Options;

/// <summary>
/// Base result
/// </summary>
public abstract class ROption : IROption
{
    protected ROption()
    { }

    protected ROption(IRError error)
        : this(error.ToList())
    { }

    protected ROption(IReadOnlyCollection<IRError> errors)
    {
        Errors = errors;
    }

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
    /// Converting to result unit
    /// </summary>
    /// <returns>Result unit</returns>
    public IRUnit ToRUnit() =>
        Success 
            ? RUnitFactory.Some() 
            : RUnitFactory.None(GetErrors());

    /// <summary>
    /// Converting to result value
    /// </summary>
    /// <typeparam name="TValue">Value</typeparam>
    /// <returns>Result value</returns>
    public IRValue<TValue> ToRValue<TValue>(TValue value)
        where TValue : notnull =>
        Success
            ? RValueFactory.Some(value)
            : RValueFactory.None<TValue>(GetErrors());

    /// <summary>
    /// Converting to result collection
    /// </summary>
    /// <typeparam name="TValue">Value</typeparam>
    /// <returns>Result collection</returns>
    public IRList<TValue> ToRList<TValue>(IReadOnlyCollection<TValue> values)
        where TValue : notnull =>
        Success
            ? RListFactory.Some(values)
            : RListFactory.None<TValue>(GetErrors());
}