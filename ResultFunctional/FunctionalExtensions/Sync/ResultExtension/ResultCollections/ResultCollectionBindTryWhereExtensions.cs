using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections.ResultCollectionBindTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Extension methods for result collection monad function with conditions and exception handling
    /// </summary>
    public static class ResultCollectionBindTryWhereExtensions
    {
        /// <summary>
        /// Execute result collection function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Monad result collection function</param>
        /// <param name="exceptionFunc">Exception function</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValueOut> ResultCollectionBindTryOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                  Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> func,
                                                                                                  Func<Exception, IRError> exceptionFunc) =>
            @this.ResultCollectionBindOk(value => ResultCollectionBindTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Execute result collection function in no error case; else catch exception
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="func">Monad result collection function</param>
        /// <param name="error">Error</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValueOut> ResultCollectionBindTryOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                  Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> func,
                                                                                                  IRError error) =>
            @this.ResultCollectionBindOk(value => ResultCollectionBindTry(() => func.Invoke(value), error));
    }
}