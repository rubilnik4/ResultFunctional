using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
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
        public static async Task<IReadOnlyCollection<TValueOut>> ResultCollectionToCollectionOkBadTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                                                Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc,
                                                                                                                                Func<IReadOnlyCollection<IErrorResult>, IEnumerable<TValueOut>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionToCollectionOkBad(okFunc, badFunc));
    }
}