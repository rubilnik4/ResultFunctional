using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;
using static ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values.RValueTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Exception handling task result value with conditions extension methods
    /// </summary>
    public static class ResultValueTryWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Execute function and handle exception with task result value concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Value function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> ResultValueTryOkTaskAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                    Func<TValueIn, TValueOut> func,
                                                                                                    Func<Exception, IRError> exceptionFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            ResultValueBindOkTaskAsync(value => RValueTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute function and handle exception with task result value concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Value function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> ResultValueTryOkTaskAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, TValueOut> func, 
                                                                                                         IRError error)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            ResultValueBindOkTaskAsync(value => RValueTry(() => func.Invoke(value), error));
    }
}