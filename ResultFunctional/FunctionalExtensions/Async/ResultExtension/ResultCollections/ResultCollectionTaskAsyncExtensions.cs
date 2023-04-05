using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
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
        public static async Task<IResultCollection<TValue>> ConcatResultCollectionTaskAsync<TValue>(this Task<IEnumerable<IResultCollection<TValue>>> @this) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ConcatResultCollection());

        /// <summary>
        /// Aggregate collection of task result collections
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection of result collection</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValue>> ConcatResultCollectionTaskAsync<TValue>(this Task<IReadOnlyCollection<IResultCollection<TValue>>> @this) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ConcatResultCollection());

        /// <summary>
        /// Aggregate collection of task result collections
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming collection of result collection</param>
        /// <returns>Outgoing result collection</returns> 
        public static async Task<IResultCollection<TValue>> ConcatResultCollectionTaskAsync<TValue>(this Task<IResultCollection<TValue>[]> @this) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ConcatResultCollection());
    }
}