﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Maybe;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe
{
    /// <summary>
    /// Result error async extension methods for merging errors
    /// </summary>
    public static class RMaybeBindOptionAsyncExtensions
    {
        /// <summary>
        /// Merge result error async function depending on incoming result error
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="someFunc">Function if result error hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRMaybe> RMaybeBindMatchAsync(this IRMaybe @this, Func<Task<IRMaybe>> someFunc,
                                                             Func<IReadOnlyCollection<IRError>, Task<IRMaybe>> noneFunc) =>
            @this.Success
                ? await someFunc.Invoke()
                : await noneFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Merge result error async function if incoming result collection hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="someFunc">Function if result error hasn't errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRMaybe> RMaybeBindSomeAsync(this IRMaybe @this, Func<Task<IRMaybe>> someFunc) =>
            @this.Success
                ? await someFunc.Invoke()
                : @this;
    }
}