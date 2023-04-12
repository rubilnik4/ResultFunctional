using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using static ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists.ResultCollectionBindTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists
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
        public static IRList<TValueOut> ResultCollectionBindTryOk<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                       Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> func,
                                                                                       Func<Exception, IRError> exceptionFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
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
        public static IRList<TValueOut> ResultCollectionBindTryOk<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                  Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> func,
                                                                                                  IRError error)
             where TValueIn : notnull
             where TValueOut : notnull =>
            @this.ResultCollectionBindOk(value => ResultCollectionBindTry(() => func.Invoke(value), error));
    }
}