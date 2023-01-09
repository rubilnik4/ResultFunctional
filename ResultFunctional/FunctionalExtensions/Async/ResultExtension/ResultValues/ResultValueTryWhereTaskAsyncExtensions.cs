using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
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
        public static async Task<IResultValue<TValueOut>> ResultValueTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, TValueOut> func,
                                                                                                         Func<Exception, IRError> exceptionFunc) =>
            await @this.
            ResultValueBindOkTaskAsync(value => ResultValueTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute function and handle exception with task result value concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Value function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValueOut>> ResultValueTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, TValueOut> func, 
                                                                                                         IRError error) =>
            await @this.
            ResultValueBindOkTaskAsync(value => ResultValueTry(() => func.Invoke(value), error));
    }
}