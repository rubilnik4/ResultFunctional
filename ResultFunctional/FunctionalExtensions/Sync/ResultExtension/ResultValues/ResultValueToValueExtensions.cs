using System;
using System.Collections.Generic;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for value function converting to value
    /// </summary>
    public static class ResultValueToValueExtensions
    {
        /// <summary>
        /// Execute value function converting to another value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <param name="badFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing value</returns>
        public static TValueOut ResultValueToValueOkBad<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                             Func<TValueIn, TValueOut> okFunc,
                                                                             Func<IReadOnlyCollection<IErrorResult>, TValueOut> badFunc) =>
            @this.OkStatus
                ? okFunc.Invoke(@this.Value)
                : badFunc.Invoke(@this.Errors);
    }
}