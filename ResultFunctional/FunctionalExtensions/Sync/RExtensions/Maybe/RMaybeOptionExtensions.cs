using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe;

/// <summary>
/// Result error extension methods for errors
/// </summary>
public static class RMaybeOptionExtensions
{
    /// <summary>
    /// Check errors by predicate to result error if ones hasn't errors
    /// </summary>
    /// <param name="this">Result error</param>
    /// <param name="predicate">Predicate function</param>
    /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
    /// <returns>Result error</returns>
    public static IRMaybe RMaybeEnsure(this IRMaybe @this, Func<bool> predicate,
                                       Func<IEnumerable<IRError>> noneFunc) =>
        @this.Success
             ? predicate()
                 ? RUnitFactory.Some()
                 : noneFunc.Invoke().ToRUnit()
             : @this.GetErrors().ToRUnit();

    /// <summary>
    /// Check errors by predicate and collect them to result
    /// </summary>
    /// <param name="this">Result error</param>
    /// <param name="predicate">Predicate function</param>
    /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
    /// <returns>Result error</returns>
    public static IRMaybe RMaybeCollect(this IRMaybe @this, Func<bool> predicate,
                                        Func<IEnumerable<IRError>> noneFunc) =>
        predicate()
            ? @this
            : @this.GetErrorsOrEmpty().Concat(noneFunc()).ToRUnit();
}