using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;

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
    public static IRUnit ResultErrorCheckErrorsOk(this IRUnit @this,
                                                  Func<bool> predicate,
                                                  Func<IEnumerable<IRError>> badFunc) =>
        @this.Success
             ? predicate()
                 ? RUnitFactory.Some()
                 : badFunc.Invoke().ToRUnit()
             : @this.GetErrors().ToRUnit();
}