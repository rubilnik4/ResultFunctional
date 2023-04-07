using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Values
{
    /// <summary>
    /// Result error async extension methods for merging errors
    /// </summary>
    public static class ResultErrorBindWhereAsyncExtensions
    {
        /// <summary>
        /// Merge result error async function depending on incoming result error
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="okFunc">Function if result error hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRUnit> ResultErrorBindOkBadAsync(this IRUnit @this, Func<Task<IRUnit>> okFunc,
                                                                   Func<IReadOnlyCollection<IRError>, Task<IRUnit>> badFunc) =>
            @this.Success
                ? await okFunc.Invoke()
                : await badFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Merge result error async function if incoming result collection hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="okFunc">Function if result error hasn't errors</param>
        /// <returns>Outgoing result error</returns>
        public static async Task<IRUnit> ResultErrorBindOkAsync(this IRUnit @this, Func<Task<IRUnit>> okFunc) =>
            @this.Success
                ? await okFunc.Invoke()
                : @this;
    }
}