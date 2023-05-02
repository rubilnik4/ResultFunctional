using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Task result collection reorder extension methods
    /// </summary>
    public static class RListLiftTaskExtensions
    {
        /// <summary>
        /// Converting task result collection to ordinal collection
        /// </summary>      
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if incoming result collection hasn't errors</param>
        /// <param name="noneFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing collection</returns> 
        public static async Task<IReadOnlyCollection<TValueOut>> RListLiftMatchTask<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> someFunc,
                                                                                                         Func<IReadOnlyCollection<IRError>, IEnumerable<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RListLiftMatch(someFunc, noneFunc));
    }
}