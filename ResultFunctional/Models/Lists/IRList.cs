using System.Collections.Generic;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Values;

namespace ResultFunctional.Models.Lists;

/// <summary>
/// Result with collection
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
public interface IRList<out TValue> : IRValue<IReadOnlyCollection<TValue>>
    where TValue : notnull
{ }