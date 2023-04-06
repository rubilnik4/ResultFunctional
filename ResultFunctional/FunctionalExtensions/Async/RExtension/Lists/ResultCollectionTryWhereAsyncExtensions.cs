using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using static ResultFunctional.FunctionalExtensions.Async.RExtension.Lists.ResultCollectionTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Lists
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
        public static async Task<IRList<TValueOut>> ResultCollectionTryOkAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                    Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> func,
                                                                                                    Func<Exception, IRError> exceptionFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
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
        public static async Task<IRList<TValueOut>> ResultCollectionTryOkAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> func,
                                                                                                     IRError error)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            ResultCollectionBindOkAsync(value => ResultCollectionTryAsync(() => func.Invoke(value), error));
    }
}