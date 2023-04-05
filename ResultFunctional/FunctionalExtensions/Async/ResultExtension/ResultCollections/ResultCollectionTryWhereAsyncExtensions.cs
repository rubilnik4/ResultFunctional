using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections.ResultCollectionTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Async exception handling result collection with conditions extension methods
    /// </summary>
    public static class ResultCollectionTryWhereAsyncExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> func,
                                                                                                     Func<Exception, IRError> exceptionFunc) =>
            await @this.ResultCollectionTryOkAsync(values => func(values).GetEnumerableTaskAsync(),
                                                   exceptionFunc);

        /// <summary>
        /// Execute async function and handle exception with result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> func,
                                                                                                     Func<Exception, IRError> exceptionFunc) =>
            await @this.
            ResultCollectionBindOkAsync(value => ResultCollectionTryAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute async function and handle exception with result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> func,
                                                                                                     IRError error) =>
            await @this.ResultCollectionTryOkAsync(values => func(values).GetEnumerableTaskAsync(),
                                                   error);

        /// <summary>
        /// Execute async function and handle exception with result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> func,
                                                                                                     IRError error) =>
            await @this.
            ResultCollectionBindOkAsync(value => ResultCollectionTryAsync(() => func.Invoke(value), error));
    }
}