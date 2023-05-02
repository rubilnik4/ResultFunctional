using ResultFunctional.Models.Base;
using ResultFunctional.Models.Units;

namespace ResultFunctional.Models.Values;

/// <summary>
/// Result with value
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
public interface IRValue<out TValue>: IRBase<TValue, IRValue<TValue>>
    where TValue : notnull
{ }