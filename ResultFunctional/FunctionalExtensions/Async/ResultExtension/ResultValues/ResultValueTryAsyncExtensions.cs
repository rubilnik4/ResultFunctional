using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Exception handling result value async extension methods
    /// </summary>
    public static class ResultValueTryAsyncExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with result value converting
        /// </summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="func">Value function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueTryAsync<TValue>(Func<Task<TValue>> func,
                                                                                   Func<Exception, IRError> exceptionFunc)
        {
            try
            {
                return new ResultValue<TValue>(await func.Invoke());
            }
            catch (Exception ex)
            {
                return new ResultValue<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Execute async function and handle exception with result value converting
        /// </summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="func">Value function</param>
        /// <param name="error">Error</param>
        /// <returns>Result value</returns>
        public static async Task<IResultValue<TValue>> ResultValueTryAsync<TValue>(Func<Task<TValue>> func, IRError error) =>
            await ResultValueTryAsync(func, error.AppendException);
    }
}