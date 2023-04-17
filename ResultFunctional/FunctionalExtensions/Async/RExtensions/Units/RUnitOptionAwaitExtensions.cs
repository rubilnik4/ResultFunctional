using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Units;

/// <summary>
/// Task result error async extension methods for errors
/// </summary>
public static class RUnitOptionAwaitExtensions
{
    /// <summary>
    /// Check errors by predicate async to task result error if ones hasn't errors
    /// </summary>
    /// <param name="this">Result error</param>
    /// <param name="predicate">Predicate function</param>
    /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
    /// <returns>Result error</returns>
    public static async Task<IRUnit> RUnitEnsureAwait(this Task<IRUnit> @this,
                                                      Func<bool> predicate,
                                                      Func<Task<IReadOnlyCollection<IRError>>> noneFunc) =>
        await @this.
            MapAwait(awaitedThis => awaitedThis.RUnitEnsureAsync(predicate, noneFunc));
}