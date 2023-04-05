using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;

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
    public static async Task<IResultError> ResultErrorCheckErrorsOkTaskAsync(this Task<IResultError> @this,
                                                                         Func<bool> predicate,
                                                                         Func<IEnumerable<IRError>> badFunc) =>
        await @this.
        MapTaskAsync(awaitedThis => awaitedThis.ResultErrorCheckErrorsOk(predicate, badFunc));
}