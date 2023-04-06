using System.Collections.Generic;
using ResultFunctional.Models.Values;

namespace ResultFunctional.Models.Lists;

/// <summary>
/// Result with collection
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
public interface IRList<out TValue> : IRValue<IReadOnlyCollection<TValue>>
    where TValue : notnull
{
    /// <summary>
    /// Converting to result value
    /// </summary>
    /// <returns>Result value</returns>
    IRValue<IReadOnlyCollection<TValue>> ToRValue();
}