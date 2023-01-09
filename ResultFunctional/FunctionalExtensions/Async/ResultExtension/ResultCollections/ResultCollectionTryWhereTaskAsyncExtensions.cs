using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections.ResultCollectionTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Exception handling task result collection with conditions extension methods
    /// </summary>
    public static class ResultCollectionTryWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Execute function and handle exception with task result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> func,
                                                                                                     Func<Exception, IRError> exceptionFunc) =>
            await @this.
            ResultCollectionBindOkTaskAsync(value => ResultCollectionTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute function and handle exception with task result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> func,
                                                                                                     IRError error) =>
            await @this.
            ResultCollectionBindOkTaskAsync(value => ResultCollectionTry(() => func.Invoke(value), error));
    }
}