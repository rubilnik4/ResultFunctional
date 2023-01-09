using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors
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
        /// <param name="okFunc">Function if result error hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result error</returns>
        public static IResultError ResultErrorBindOkBad(this IResultError @this, Func<IResultError> okFunc,
                                                        Func<IReadOnlyCollection<IRError>, IResultError> badFunc) =>
            @this.OkStatus
                ? okFunc.Invoke()
                : badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Merge result error function if incoming result collection hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="okFunc">Function if result error hasn't errors</param>
        /// <returns>Outgoing result error</returns>
        public static IResultError ResultErrorBindOk(this IResultError @this, Func<IResultError> okFunc) =>
            @this.OkStatus
                ? okFunc.Invoke()
                : @this;
    }
}