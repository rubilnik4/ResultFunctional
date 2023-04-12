using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units
{
    /// <summary>
    /// Result error extension methods for merging errors
    /// </summary>
    public static class ResultErrorBindWhereExtensions
    {
        /// <summary>
        /// Merge result error function depending on incoming result error
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="someFunc">Function if result error hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result error</returns>
        public static IRUnit ResultErrorBindOkBad(this IRUnit @this, Func<IRUnit> someFunc,
                                                  Func<IReadOnlyCollection<IRError>, IRUnit> noneFunc) =>
            @this.Success
                ? someFunc.Invoke()
                : noneFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Merge result error function if incoming result collection hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="someFunc">Function if result error hasn't errors</param>
        /// <returns>Outgoing result error</returns>
        public static IRUnit ResultErrorBindOk(this IRUnit @this, Func<IRUnit> someFunc) =>
            @this.Success
                ? someFunc.Invoke()
                : @this;
    }
}