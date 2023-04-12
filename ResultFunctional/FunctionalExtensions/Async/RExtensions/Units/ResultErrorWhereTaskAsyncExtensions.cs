using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Units;

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
    /// <param name="badFunc">Function if predicate <see langword="false"/></param>
    /// <returns>Result error</returns>
    public static async Task<IRUnit> ResultErrorCheckErrorsOkTaskAsync(this Task<IRUnit> @this, Func<bool> predicate,
                                                                         Func<IReadOnlyCollection<IRError>> badFunc) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ResultErrorCheckErrorsOk(predicate, badFunc));
}