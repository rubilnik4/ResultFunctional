using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Maybe
{
    /// <summary>
    /// Result error extension methods for merging errors
    /// </summary>
    public static class RMaybeBindOptionExtensions
    {
        /// <summary>
        /// Merge result error function depending on incoming result error
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="someFunc">Function if result error hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result error</returns>
        public static IRMaybe RMaybeBindMatch(this IRMaybe @this, Func<IRMaybe> someFunc,
                                              Func<IReadOnlyCollection<IRError>, IRMaybe> noneFunc) =>
            @this.Success
                ? someFunc.Invoke()
                : noneFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Merge result error function if incoming result collection hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="someFunc">Function if result error hasn't errors</param>
        /// <returns>Outgoing result error</returns>
        public static IRMaybe RMaybeBindSome(this IRMaybe @this, Func<IRMaybe> someFunc) =>
            @this.Success
                ? someFunc.Invoke()
                : @this;
    }
}