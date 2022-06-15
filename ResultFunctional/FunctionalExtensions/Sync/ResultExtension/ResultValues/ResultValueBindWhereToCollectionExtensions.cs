using System;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Extension methods for result value monad function converting to result collection
    /// </summary>
    public static class ResultValueBindWhereToCollectionExtensions
    {
        /// <summary>
        /// Execute monad result value function converting to result collection if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IResultCollection<TValueOut> ResultValueBindOkToCollection<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                             Func<TValueIn, IResultCollection<TValueOut>> okFunc) =>
            @this.
            ResultValueBindOk(okFunc).
            ToResultCollection();
    }
}