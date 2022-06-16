using System;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value replacing function
    /// </summary>
    public static class ResultValueRawWhereExtensions
    {
        /// <summary>
        /// Execute replacing result value function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultValueRawOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<IResultValue<TValueIn>, IResultValue<TValueOut>> okFunc) =>
            @this.OkStatus
                ? okFunc.Invoke(@this)
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Execute replacing result value function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="badFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValue> ResultValueRawBad<TValue>(this IResultValue<TValue> @this,
                                                                     Func<IResultValue<TValue>, IResultValue<TValue>> badFunc) =>
            @this.OkStatus
                ? new ResultValue<TValue>(@this.Value)
                : badFunc.Invoke(@this);
    }
}