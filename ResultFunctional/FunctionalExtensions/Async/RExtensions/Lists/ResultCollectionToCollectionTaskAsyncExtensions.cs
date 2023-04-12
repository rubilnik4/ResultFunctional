using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Lists
{
    /// <summary>
    /// Task result collection reorder extension methods
    /// </summary>
    public static class ResultCollectionToCollectionTaskAsyncExtensions
    {
        /// <summary>
        /// Converting task result collection to ordinal collection
        /// </summary>      
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if incoming result collection hasn't errors</param>
        /// <param name="badFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing collection</returns> 
        public static async Task<IReadOnlyCollection<TValueOut>> ResultCollectionToCollectionOkBadTaskAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                                                Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> okFunc,
                                                                                                                                Func<IReadOnlyCollection<IRError>, IReadOnlyCollection<TValueOut>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionToCollectionOkBad(okFunc, badFunc));
    }
}