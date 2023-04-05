using ResultFunctional.Models.Options;
using ResultFunctional.Models.Units;

namespace ResultFunctional.Models.Values;

/// <summary>
/// Result with value
/// </summary>
/// <typeparam name="TValue">Value</typeparam>
public interface IRValue<out TValue>: IROption<TValue, IRValue<TValue>>
    where TValue : notnull
{
    /// <summary>
    /// Convert to result unit
    /// </summary>
    /// <returns>Result unit></returns>
    IRUnit ToRUnit();
}