using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Task result collections extension methods
    /// </summary>
    public static class ResultCollectionTaskAsyncExtensions
    {
        /// <summary>
        /// Aggregate collection of task result collections
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection of result collection</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ConcatResultCollectionTaskAsync<TValue>(this Task<IEnumerable<IRList<TValue>>> @this)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.RListFold());

        /// <summary>
        /// Aggregate collection of task result collections
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection of result collection</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ConcatResultCollectionTaskAsync<TValue>(this Task<IReadOnlyCollection<IRList<TValue>>> @this)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.RListFold());

        /// <summary>
        /// Aggregate collection of task result collections
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection of result collection</param>
        /// <returns>Outgoing result collection</returns> 
        public static async Task<IRList<TValue>> ConcatResultCollectionTaskAsync<TValue>(this Task<IRList<TValue>[]> @this)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.RListFold());
    }
}