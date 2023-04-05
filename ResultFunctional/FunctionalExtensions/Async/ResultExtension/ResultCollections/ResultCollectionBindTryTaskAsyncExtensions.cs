using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Extension methods for result collection monad task function with exception handling
    /// </summary>
    public static class ResultCollectionBindTryTaskAsyncExtensions
    {
        /// <summary>
        /// Execute result collection monad task function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result collection function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionBindTryTaskAsync<TValue>(Func<Task<IRList<TValue>>> func,
                                                                                          Func<Exception, IRError> exceptionFunc)
            where TValue : notnull
        {
            try
            {
                return await func.Invoke();
            }
            catch (Exception ex)
            {
                return exceptionFunc(ex).ToRList<TValue>();
            }
        }

        /// <summary>
        /// Execute result collection monad task function with exception handling
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="func">Result collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionBindTryTaskAsync<TValue>(Func<Task<IRList<TValue>>> func,
                                                                                          IRError error)
            where TValue : notnull =>
            await ResultCollectionBindTryTaskAsync(func, error.AppendException);
    }
}