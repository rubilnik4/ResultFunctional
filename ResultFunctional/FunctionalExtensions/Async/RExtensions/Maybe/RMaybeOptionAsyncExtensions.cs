﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Units;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe;

/// <summary>
/// Result error async extension methods for errors
/// </summary>
public static class RMaybeOptionAsyncExtensions
{
    /// <summary>
    /// Check errors by predicate async to result error if ones hasn't errors
    /// </summary>
    /// <param name="this">Result error</param>
    /// <param name="predicate">Predicate function</param>
    /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
    /// <returns>Result error</returns>
    public static async Task<IRMaybe> RUnitEnsureAsync(this IRMaybe @this, Func<bool> predicate,
                                                      Func<Task<IReadOnlyCollection<IRError>>> noneFunc) =>
        @this.Success
            ? predicate()
                ? RUnit.Some()
                : await noneFunc.Invoke().ToRUnitTask()
            : @this.GetErrors().ToRUnit();
}