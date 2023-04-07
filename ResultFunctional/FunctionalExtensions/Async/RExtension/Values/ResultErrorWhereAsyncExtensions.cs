using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Values;

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
    public static async Task<IRUnit> ResultErrorCheckErrorsOkAsync(this IRUnit @this,
                                                                         Func<bool> predicate,
                                                                         Func<Task<IReadOnlyCollection<IRError>>> badFunc) =>
         @this.Success
             ? predicate()
                 ? RUnit.Some()
                 : await badFunc.Invoke().ToRUnitTaskAsync()
             : @this.GetErrors().ToRUnit();
}