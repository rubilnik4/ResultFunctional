using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for value async function converting to value
    /// </summary>
    public static class ResultValueToValueAsyncExtensions
    {
        /// <summary>
        /// Execute value async function converting to another value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <param name="badFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing value</returns>
        public static async Task<TValueOut> ResultValueToValueOkBadAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                    Func<IReadOnlyCollection<IErrorResult>, Task<TValueOut>> badFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke(@this.Value)
                : await badFunc.Invoke(@this.Errors);
    }
}