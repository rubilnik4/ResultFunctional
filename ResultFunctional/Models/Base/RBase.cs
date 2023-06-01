using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;

namespace ResultFunctional.Models.Base;

/// <summary>
/// Base result with value
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
/// <typeparam name="TOption">Result option</typeparam>
public abstract class RBase<TValue, TOption> : RMaybe, IRBase<TValue, TOption>
    where TValue : notnull
    where TOption : IRBase<TValue, TOption>
{
    protected RBase(TValue value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    protected RBase(IRError error)
        : this(error.ToList())
    { }

    protected RBase(IReadOnlyCollection<IRError> errors)
        : base(errors)
    { }

    /// <summary>
    /// Value
    /// </summary>
    public TValue? Value { get; }

    /// <summary>
    /// Initialize result by value
    /// </summary>
    /// <param name="value">Value</param>
    /// <returns>Result option</returns>
    protected abstract TOption Initialize(TValue value);

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
            .Option(errorList => errorList.Count > 0,
                           Initialize,
                           _ => Initialize(GetValue()));
}