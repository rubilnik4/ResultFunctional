using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values
{
    /// <summary>
    /// Extension methods for value function converting to value
    /// </summary>
    public static class RValueUnWrapExtensions
    {
        /// <summary>
        /// Execute value function converting to another value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if incoming result value hasn't errors</param>
        /// <param name="noneFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing value</returns>
        public static TValueOut RValueUnWrapMatch<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                             Func<TValueIn, TValueOut> someFunc,
                                                                             Func<IReadOnlyCollection<IRError>, TValueOut> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? someFunc.Invoke(@this.GetValue())
                : noneFunc.Invoke(@this.GetErrors());
    }
}