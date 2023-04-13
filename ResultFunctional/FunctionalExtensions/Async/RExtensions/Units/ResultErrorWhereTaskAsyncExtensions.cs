using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Units;

/// <summary>
/// Task result error extension methods for errors
/// </summary>
public static class ResultErrorWhereBindAsyncExtensions
{
    /// <summary>
    /// Check errors by predicate to task result error if ones hasn't errors
    /// </summary>
    /// <param name="this">Result error</param>
    /// <param name="predicate">Predicate function</param>
    /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
    /// <returns>Result error</returns>
    public static async Task<IRUnit> ResultErrorCheckErrorsOkTaskAsync(this Task<IRUnit> @this, Func<bool> predicate,
                                                                         Func<IReadOnlyCollection<IRError>> noneFunc) =>
        await @this.
        MapTask(awaitedThis => awaitedThis.RUnitEnsure(predicate, noneFunc));
}