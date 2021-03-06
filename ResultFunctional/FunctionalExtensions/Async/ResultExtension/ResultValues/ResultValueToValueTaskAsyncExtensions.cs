using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for task value function converting to value
    /// </summary>
    public static class ResultValueToValueTaskAsyncExtensions
    {
        /// <summary>
        /// Execute task value function converting to another value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <param name="badFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing value</returns>
        public static async Task<TValueOut> ResultValueToValueOkBadTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                    Func<TValueIn, TValueOut> okFunc,
                                                                                    Func<IReadOnlyCollection<IErrorResult>, TValueOut> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultValueToValueOkBad(okFunc, badFunc));
    }
}