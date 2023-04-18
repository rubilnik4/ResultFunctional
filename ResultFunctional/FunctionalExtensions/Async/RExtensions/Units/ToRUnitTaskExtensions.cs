using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Units
{
    /// <summary>
    /// Result unit task extension methods
    /// </summary>  
    public static class ToRUnitTaskExtensions
    {
        /// <summary>
        /// Merge task result errors collection
        /// </summary>
        /// <param name="this">Result error collection</param>
        /// <returns>Result error</returns>
        public static async Task<IRUnit> ToRUnitTask(this Task<IEnumerable<IRUnit>> @this) =>
            await @this.
            MapTask(awaitedThis => awaitedThis.ToRUnit());

        /// <summary>
        /// Merge task result errors collection
        /// </summary>
        /// <param name="this">Result error collection</param>
        /// <returns>Result error</returns>
        public static async Task<IRUnit> ToRUnitTask(this Task<IReadOnlyCollection<IRUnit>> @this) =>
            await @this.
            MapTask(awaitedThis => (IEnumerable<IRUnit>)awaitedThis).
            ToRUnitTask();

        /// <summary>
        /// Merge errors collection
        /// </summary>
        /// <param name="this">Error collection</param>
        /// <returns>Result error</returns>
        public static async Task<IRUnit> ToRUnitTask(this Task<IEnumerable<IRError>> @this) =>
            await @this.
            MapTask(awaitedThis => awaitedThis.ToRUnit());

        /// <summary>
        /// Merge errors collection
        /// </summary>
        /// <param name="this">Error collection</param>
        /// <returns>Result error</returns>
        public static async Task<IRUnit> ToRUnitTask(this Task<IReadOnlyCollection<IRError>> @this) =>
            await @this.
            MapTask(awaitedThis => (IEnumerable<IRError>)awaitedThis).
            ToRUnitTask();

        /// <summary>
        /// Merge result unit collection
        /// </summary>
        /// <param name="this">Result unit collection</param>
        /// <returns>Result unit</returns>
        public static async Task<IRUnit> ToRUnitTask(this IEnumerable<Task<IRUnit>> @this) =>
            await Task.WhenAll(@this).
                       MapTask(result => result.ToRUnit());

        /// <summary>
        /// Converting result value to unit
        /// </summary>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="this">Result value</param>
        /// <returns>Result unit</returns>
        public static async Task<IRUnit> ToRUnitTask<TValue>(this Task<IRValue<TValue>> @this)
            where TValue : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.ToRUnit());
    }
}