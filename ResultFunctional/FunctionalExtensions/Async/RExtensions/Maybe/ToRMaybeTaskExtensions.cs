using System.Threading.Tasks;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe;

/// <summary>
/// Result maybe task extension methods
/// </summary>  
public static class ToRMaybeTaskExtensions
{
    /// <summary>
    /// Converting result unit to option
    /// </summary>
    /// <param name="this">Result unit</param>
    /// <returns>Result option</returns>
    public static async Task<IRMaybe> ToRMaybeTask(this Task<IRUnit> @this) =>
        await @this.MapTask(awaited => awaited);

    /// <summary>
    /// Converting result value to option
    /// </summary>
    /// <param name="this">Result unit</param>
    /// <returns>Result option</returns>
    public static async Task<IRMaybe> ToRMaybeTask<TValue>(this Task<IRValue<TValue>> @this)
            where TValue : notnull =>
        await @this.MapTask(awaited => awaited);

    /// <summary>
    /// Converting result collection to option
    /// </summary>
    /// <param name="this">Result unit</param>
    /// <returns>Result collection</returns>
    public static async Task<IRMaybe> ToRMaybeTask<TValue>(this Task<IRList<TValue>> @this)
            where TValue : notnull =>
        await @this.MapTask(awaited => awaited);
}