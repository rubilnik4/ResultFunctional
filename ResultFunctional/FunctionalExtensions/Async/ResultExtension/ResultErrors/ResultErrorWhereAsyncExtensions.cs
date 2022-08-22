using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;

/// <summary>
/// Result error async extension methods for errors
/// </summary>
public static class ResultErrorWhereAsyncExtensions
{
    /// <summary>
    /// Check errors by predicate async to result error if ones hasn't errors
    /// </summary>
    /// <param name="this">Result error</param>
    /// <param name="predicate">Predicate function</param>
    /// <param name="badFunc">Function if predicate <see langword="false"/></param>
    /// <returns>Result error</returns>
    public static async Task<IResultError> ResultErrorCheckErrorsOkAsync(this IResultError @this,
                                                                         Func<bool> predicate,
                                                                         Func<Task<IReadOnlyCollection<IErrorResult>>> badFunc) =>
        await @this.ResultErrorCheckErrorsOkAsync(predicate,
                                                  () => badFunc().GetEnumerableTaskAsync());

    /// <summary>
    /// Check errors by predicate async to result error if ones hasn't errors
    /// </summary>
    /// <param name="this">Result error</param>
    /// <param name="predicate">Predicate function</param>
    /// <param name="badFunc">Function if predicate <see langword="false"/></param>
    /// <returns>Result error</returns>
    public static async Task<IResultError> ResultErrorCheckErrorsOkAsync(this IResultError @this,
                                                                         Func<bool> predicate,
                                                                         Func<Task<IEnumerable<IErrorResult>>> badFunc) =>
        @this.OkStatus
             ? predicate()
                 ? new ResultError()
                 : new ResultError(await badFunc.Invoke())
             : new ResultError(@this.Errors);
}