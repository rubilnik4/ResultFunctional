using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Values;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues
{
    /// <summary>
    /// Extension methods for task result value monad function converting to result collection
    /// </summary>
    public static class ResultValueBindWhereToCollectionTaskAsyncExtensions
    {
        /// <summary>
        /// Execute monad task result value function converting to result collection if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultValueBindOkToCollectionTaskAsync<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                                                Func<TValueIn, IRList<TValueOut>> okFunc) 
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ResultValueBindOkToCollection(okFunc));
    }
}