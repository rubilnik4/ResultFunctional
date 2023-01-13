using System.Collections.Generic;
using ResultFunctional.Models.Options;

namespace ResultFunctional.Models.Lists;

/// <summary>
/// Result with collection
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
public interface IRList<out TValue> : IROption<IReadOnlyCollection<TValue>>
    where TValue : notnull
{ }