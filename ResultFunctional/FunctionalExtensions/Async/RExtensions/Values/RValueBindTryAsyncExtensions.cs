using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Extension methods for result value async monad function with exception handling
    /// </summary>
    public static class RValueBindTryAsyncExtensions
    {
        /// <summary>
        /// Execute result value async monad function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result value function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Result value</returns>
        public static async Task<IRValue<TValue>> RValueBindTryAsync<TValue>(Func<Task<IRValue<TValue>>> func,
                                                                             Func<Exception, IRError> exceptionFunc)
            where TValue : notnull
        {
            try
            {
                return await func.Invoke();
            }
            catch (Exception ex)
            {
                return exceptionFunc(ex).ToRValue<TValue>();
            }
        }

        /// <summary>
        /// Execute result collection async monad function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result value function</param>
        /// <param name="error">Error</param>
        /// <returns>Result value</returns>
        public static async Task<IRValue<TValue>> RValueBindTryAsync<TValue>(Func<Task<IRValue<TValue>>> func,
                                                                                  IRError error)
            where TValue : notnull =>
             await RValueBindTryAsync(func, error.AppendException);
    }
}