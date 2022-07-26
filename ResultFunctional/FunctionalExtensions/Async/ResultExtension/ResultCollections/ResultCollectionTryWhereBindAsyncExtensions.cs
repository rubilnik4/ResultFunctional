using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections.ResultCollectionTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Async exception handling task result collection with conditions extension methods
    /// </summary>
    public static class ResultCollectionTryWhereBindAsyncExtensions
    {
        /// <summary>
        /// Execute async function and handle exception with task result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> func,
                                                                                                     Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.ResultCollectionTryOkBindAsync(values => func(values).GetEnumerableTaskAsync(),
                                                       exceptionFunc);

        /// <summary>
        /// Execute async function and handle exception with task result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> func,
                                                                                                     Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.
            ResultCollectionBindOkBindAsync(value => ResultCollectionTryAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute async function and handle exception with task result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> func,
                                                                                                     IErrorResult error) =>
            await @this.ResultCollectionTryOkBindAsync(values => func(values).GetEnumerableTaskAsync(),
                                                       error);

        /// <summary>
        /// Execute async function and handle exception with task result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> func,
                                                                                                     IErrorResult error) =>
            await @this.
            ResultCollectionBindOkBindAsync(value => ResultCollectionTryAsync(() => func.Invoke(value), error));
    }
}