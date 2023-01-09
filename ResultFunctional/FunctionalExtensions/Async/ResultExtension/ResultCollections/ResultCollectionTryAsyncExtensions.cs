using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Async exception handling result collection extension methods
    /// </summary>
    public static class ResultCollectionTryAsyncExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with result collection converting
        /// </summary>
        /// <typeparam name="TValue">Collection type</typeparam>
        /// <param name="func">Collection function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionTryAsync<TValue>(Func<Task<IEnumerable<TValue>>> func,
                                                                                             Func<Exception, IRError> exceptionFunc)
        {
            try
            {
                return new ResultCollection<TValue>(await func.Invoke());
            }
            catch (Exception ex)
            {
                return new ResultCollection<TValue>(exceptionFunc(ex));
            }
        }

        /// <summary>
        /// Execute async function and handle exception with result collection converting
        /// </summary>
        /// <typeparam name="TValue">Collection type</typeparam>
        /// <param name="func">Collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionTryAsync<TValue>(Func<Task<IEnumerable<TValue>>> func,
                                                                                             IRError error) =>
            await ResultCollectionTryAsync(func, error.AppendException);

        /// <summary>
        /// Execute async function and handle exception with result collection converting
        /// </summary>
        /// <typeparam name="TValue">Collection type</typeparam>
        /// <param name="func">Collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionTryAsync<TValue>(Func<Task<IReadOnlyCollection<TValue>>> func,
                                                                                             IRError error) =>
            await ResultCollectionTryAsync(async () => (IEnumerable<TValue>)await func.Invoke(), error);

        /// <summary>
        /// Execute async function and handle exception with result collection converting
        /// </summary>
        /// <typeparam name="TValue">Collection type</typeparam>
        /// <param name="func">Collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Result collection</returns>
        public static async Task<IResultCollection<TValue>> ResultCollectionTryAsync<TValue>(Func<Task<List<TValue>>> func,
                                                                                             IRError error) =>
            await ResultCollectionTryAsync(async () => (IEnumerable<TValue>)await func.Invoke(), error);
    }
}