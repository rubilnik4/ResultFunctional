using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe;

/// <summary>
/// Task result error async extension methods for errors
/// </summary>
public static class RMaybeOptionAwaitExtensions
{
    /// <summary>
    /// Check errors by predicate async to task result error if ones hasn't errors
    /// </summary>
    /// <param name="this">Result error</param>
    /// <param name="predicate">Predicate function</param>
    /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
    /// <returns>Result error</returns>
    public static async Task<IRMaybe> RMaybeEnsureAwait(this Task<IRMaybe> @this, Func<bool> predicate,
                                                      Func<Task<IReadOnlyCollection<IRError>>> noneFunc) =>
        await @this.
            MapAwait(awaitedThis => awaitedThis.RMaybeEnsureAsync(predicate, noneFunc));

    /// <summary>
    /// Check errors by predicate to task and collect them to result
    /// </summary>
    /// <param name="this">Result error</param>
    /// <param name="predicate">Predicate function</param>
    /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
    /// <returns>Result error</returns>
    public static async Task<IRMaybe> RMaybeConcatAwait(this Task<IRMaybe> @this, Func<bool> predicate,
                                                         Func<Task<IReadOnlyCollection<IRError>>> noneFunc) =>
        predicate()
            ? await @this
            : await @this
               .MapAwait(awaitedThis => awaitedThis.RMaybeConcatAsync(predicate, noneFunc));
}