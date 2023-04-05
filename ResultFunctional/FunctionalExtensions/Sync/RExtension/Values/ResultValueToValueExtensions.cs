using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Values
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
        public static TValueOut ResultValueToValueOkBad<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                             Func<TValueIn, TValueOut> okFunc,
                                                                             Func<IReadOnlyCollection<IRError>, TValueOut> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? okFunc.Invoke(@this.GetValue())
                : badFunc.Invoke(@this.GetErrors());
    }
}