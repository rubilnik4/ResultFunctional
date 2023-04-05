using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Lists
{
    /// <summary>
    /// Extension methods for result collection monad async task function with conditions and exception handling
    /// </summary>
    public static class ResultCollectionBindTryWhereBindAsyncExtensions
    {
        /// <summary>
        /// Execute result collection async task function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Monad result collection function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindTryOkBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                       Func<IEnumerable<TValueIn>, Task<IResultCollection<TValueOut>>> func,
                                                                                                       Func<Exception, IRError> exceptionFunc) =>
            await @this.
            ResultCollectionBindOkBindAsync(value => ResultCollectionBindTryTaskAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute result collection async task function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Monad result collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindTryOkBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                       Func<IEnumerable<TValueIn>, Task<IResultCollection<TValueOut>>> func,
                                                                                                       IRError error) =>
            await @this.
            ResultCollectionBindOkBindAsync(value => ResultCollectionBindTryTaskAsync(() => func.Invoke(value), error));
    }
}