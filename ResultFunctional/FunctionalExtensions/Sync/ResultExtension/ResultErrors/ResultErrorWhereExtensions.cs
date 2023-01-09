using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;

/// <summary>
/// Result error extension methods for errors
/// </summary>
public static class ResultErrorWhereExtensions
{
    /// <summary>
    /// Check errors by predicate to result error if ones hasn't errors
    /// </summary>
    /// <param name="this">Result error</param>
    /// <param name="predicate">Predicate function</param>
    /// <param name="badFunc">Function if predicate <see langword="false"/></param>
    /// <returns>Result error</returns>
    public static IResultError ResultErrorCheckErrorsOk(this IResultError @this,
                                                        Func<bool> predicate,
                                                        Func<IEnumerable<IRError>> badFunc) =>
        @this.OkStatus
             ? predicate()
                 ? new ResultError()
                 : new ResultError(badFunc.Invoke())
             : new ResultError(@this.Errors);
}