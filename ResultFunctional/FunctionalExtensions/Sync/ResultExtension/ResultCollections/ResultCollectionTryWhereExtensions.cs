using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections.ResultCollectionTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Exception handling result collection with conditions extension methods
    /// </summary>
    public static class ResultCollectionTryWhereExtensions
    {
        /// <summary>
        /// Execute function and handle exception with result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="exceptionFunc">Function converting exception to error</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValueOut> ResultCollectionTryOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                              Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> func,
                                                                                              Func<Exception, IRError> exceptionFunc) =>
            @this.ResultCollectionBindOk(value => ResultCollectionTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute function and handle exception with result collection concat
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValueOut> ResultCollectionTryOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                  Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> func,
                                                                                                  IRError error) =>
            @this.ResultCollectionBindOk(value => ResultCollectionTry(() => func.Invoke(value), error));
    }
}