using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Values;

/// <summary>
/// Task result error async extension methods for errors
/// </summary>
public static class ResultErrorWhereTaskAsyncExtensions
{
    /// <summary>
    /// Check errors by predicate async to task result error if ones hasn't errors
    /// </summary>
    /// <param name="this">Result error</param>
    /// <param name="predicate">Predicate function</param>
    /// <param name="badFunc">Function if predicate <see langword="false"/></param>
    /// <returns>Result error</returns>
    public static async Task<IRUnit> ResultErrorCheckErrorsOkBindAsync(this Task<IRUnit> @this,
                                                                         Func<bool> predicate,
                                                                         Func<Task<IReadOnlyCollection<IRError>>> badFunc) =>
         await @this.
        MapBindAsync(awaitedThis => awaitedThis.ResultErrorCheckErrorsOkAsync(predicate, badFunc));
}