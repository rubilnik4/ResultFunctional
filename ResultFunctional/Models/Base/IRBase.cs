using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Options;

namespace ResultFunctional.Models.Base;

/// <summary>
/// Base result with value
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
/// <typeparam name="TOption">Base result</typeparam>
public interface IRBase<out TValue, out TOption>: IROption
    where TValue : notnull
    where TOption : IRBase<TValue, TOption>
{
    /// <summary>
    /// Value
    /// </summary>
    TValue? Value { get; }

    /// <summary>
    /// Get value
    /// </summary>
    /// <returns>Value</returns>
    /// <exception cref="ArgumentNullException">Throw exception if value not found</exception>
    TValue GetValue();

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