using ResultFunctional.Models.Options;

namespace ResultFunctional.Models.Values;

/// <summary>
/// Result with value
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
public interface IRValue<out TValue>: IROption<TValue, IRValue<TValue>>
    where TValue : notnull
{ }