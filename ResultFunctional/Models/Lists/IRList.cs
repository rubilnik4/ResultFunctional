using System.Collections;
using System.Collections.Generic;
using ResultFunctional.Models.Base;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.Models.Lists;

/// <summary>
/// Result with collection
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
public interface IRList<out TValue> : IRBase<IReadOnlyCollection<TValue>, IRList<TValue>>
    where TValue : notnull
{
    /// <summary>
    /// Converting to result value
    /// </summary>
    /// <returns>Result value</returns>
    IRValue<IReadOnlyCollection<TValue>> ToRValue();
}