using System;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Values
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
        public static IRList<TValueOut> ResultValueBindOkToCollection<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                           Func<TValueIn, IRList<TValueOut>> okFunc) 
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.
            ResultValueBindOk(okFunc).
            ToRList();
    }
}