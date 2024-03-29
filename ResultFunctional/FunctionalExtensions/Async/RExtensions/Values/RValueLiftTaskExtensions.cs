﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Extension methods for task value function converting to value
    /// </summary>
    public static class RValueLiftTaskExtensions
    {
        /// <summary>
        /// Execute task value function converting to another value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if incoming result value hasn't errors</param>
        /// <param name="noneFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing value</returns>
        public static async Task<TValueOut> RValueLiftMatchTask<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                          Func<TValueIn, TValueOut> someFunc,
                                                                                          Func<IReadOnlyCollection<IRError>, TValueOut> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.RValueLiftMatch(someFunc, noneFunc));
    }
}