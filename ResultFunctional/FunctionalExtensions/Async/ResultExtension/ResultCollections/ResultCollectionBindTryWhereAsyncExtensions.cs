using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections.ResultCollectionBindTryTaskAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Extension methods for result collection monad async function with conditions and exception handling
    /// </summary>
    public static class ResultCollectionBindTryWhereAsyncExtensions
    {
        /// <summary>
        /// Execute result collection async function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Monad result collection function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindTryOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                       Func<IEnumerable<TValueIn>, Task<IResultCollection<TValueOut>>> func,
                                                                                                       Func<Exception, IRError> exceptionFunc) =>
            await @this.
            ResultCollectionBindOkAsync(value => ResultCollectionBindTryTaskAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute result collection async function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Monad result collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindTryOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                       Func<IEnumerable<TValueIn>, Task<IResultCollection<TValueOut>>> func,
                                                                                                       IRError error) =>
            await @this.
            ResultCollectionBindOkAsync(value => ResultCollectionBindTryTaskAsync(() => func.Invoke(value), error));
    }
}