using System;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Exception handling result value with conditions extension methods
    /// </summary>
    public static class ResultValueTryWhereExtensions
    {
        /// <summary>
        /// Execute function and handle exception with result value concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Value function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultValueTryOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, TValueOut> func,
                                                                                    Func<Exception, IErrorResult> exceptionFunc) =>
            @this.ResultValueBindOk(value => ResultValueTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute function and handle exception with result value concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="func">Value function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result value</returns>
        public static IResultValue<TValueOut> ResultValueTryOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, TValueOut> func,
                                                                                    IErrorResult error) =>
            @this.ResultValueBindOk(value => ResultValueTry(() => func.Invoke(value), error));
    }
}