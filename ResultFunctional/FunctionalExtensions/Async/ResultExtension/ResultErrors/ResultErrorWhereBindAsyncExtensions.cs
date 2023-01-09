using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;

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
    public static async Task<IResultError> ResultErrorCheckErrorsOkBindAsync(this Task<IResultError> @this,
                                                                         Func<bool> predicate,
                                                                         Func<Task<IReadOnlyCollection<IRError>>> badFunc) =>
        await @this.ResultErrorCheckErrorsOkBindAsync(predicate,
                                                  () => badFunc().GetEnumerableTaskAsync());

    /// <summary>
    /// Check errors by predicate async to task result error if ones hasn't errors
    /// </summary>
    /// <param name="this">Result error</param>
    /// <param name="predicate">Predicate function</param>
    /// <param name="badFunc">Function if predicate <see langword="false"/></param>
    /// <returns>Result error</returns>
    public static async Task<IResultError> ResultErrorCheckErrorsOkBindAsync(this Task<IResultError> @this,
                                                                         Func<bool> predicate,
                                                                         Func<Task<IEnumerable<IRError>>> badFunc) =>
        await @this.
        MapBindAsync(awaitedThis => awaitedThis.ResultErrorCheckErrorsOkAsync(predicate, badFunc));
}