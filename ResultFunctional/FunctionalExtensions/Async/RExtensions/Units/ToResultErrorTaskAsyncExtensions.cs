using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Units
{
    /// <summary>
    /// Result error task extension methods
    /// </summary>  
    public static class ToResultErrorTaskAsyncExtensions
    {
        /// <summary>
        /// Merge task result errors collection
        /// </summary>
        /// <param name="this">Result error collection</param>
        /// <returns>Result error</returns>
        public static async Task<IRUnit> ToRUnitTaskAsync(this Task<IEnumerable<IRUnit>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToRUnit());

        /// <summary>
        /// Merge task result errors collection
        /// </summary>
        /// <param name="this">Result error collection</param>
        /// <returns>Result error</returns>
        public static async Task<IRUnit> ToRUnitTaskAsync(this Task<IReadOnlyCollection<IRUnit>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => (IEnumerable<IRUnit>)awaitedThis).
            ToRUnitTaskAsync();

        /// <summary>
        /// Merge errors collection
        /// </summary>
        /// <param name="this">Error collection</param>
        /// <returns>Result error</returns>
        public static async Task<IRUnit> ToRUnitTaskAsync(this Task<IEnumerable<IRError>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToRUnit());

        /// <summary>
        /// Merge errors collection
        /// </summary>
        /// <param name="this">Error collection</param>
        /// <returns>Result error</returns>
        public static async Task<IRUnit> ToRUnitTaskAsync(this Task<IReadOnlyCollection<IRError>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => (IEnumerable<IRError>)awaitedThis).
            ToRUnitTaskAsync();

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>  
        public static async Task<IRUnit> ToRUnitTaskAsync(this IEnumerable<Task<IRUnit>> @this) =>
            await Task.WhenAll(@this).
                       MapTaskAsync(result => result.ToRUnit());

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>      
        public static async Task<IRUnit> ToRUnitTaskAsync<TValue>(this Task<IRValue<TValue>> @this)
            where TValue : notnull =>
            await @this.
                MapTaskAsync(awaitedThis => awaitedThis.ToRUnit());
    }
}