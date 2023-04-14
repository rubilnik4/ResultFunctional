using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Extension methods for task value async function converting to value
    /// </summary>
    public static class RValueLiftAwaitExtensions
    {
        /// <summary>
        /// Execute task value async function converting to another value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if incoming result value hasn't errors</param>
        /// <param name="noneFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing value</returns>
        public static async Task<TValueOut> RValueLiftMatchAwait<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                    Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, Task<TValueOut>> noneFunc) 
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.RValueLiftMatchAsync(someFunc, noneFunc));
    }
}