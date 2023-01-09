using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
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
        public static async Task<IResultError> ToResultErrorTaskAsync(this Task<IEnumerable<IResultError>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultError());

        /// <summary>
        /// Merge task result errors collection
        /// </summary>
        /// <param name="this">Result error collection</param>
        /// <returns>Result error</returns>
        public static async Task<IResultError> ToResultErrorTaskAsync(this Task<IReadOnlyCollection<IResultError>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => (IEnumerable<IResultError>)awaitedThis).
            ToResultErrorTaskAsync();

        /// <summary>
        /// Merge errors collection
        /// </summary>
        /// <param name="this">Error collection</param>
        /// <returns>Result error</returns>
        public static async Task<IResultError> ToResultErrorTaskAsync(this Task<IEnumerable<IRError>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultError());

        /// <summary>
        /// Merge errors collection
        /// </summary>
        /// <param name="this">Error collection</param>
        /// <returns>Result error</returns>
        public static async Task<IResultError> ToResultErrorTaskAsync(this Task<IReadOnlyCollection<IRError>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => (IEnumerable<IRError>)awaitedThis).
            ToResultErrorTaskAsync();

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>  
        public static async Task<IResultError> ToResultErrorTaskAsync(this IEnumerable<Task<IResultError>> @this) =>
            await Task.WhenAll(@this).
                       MapTaskAsync(result => result.ToResultError());

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ToResultErrorTaskAsync<TValue>(this Task<IResultValue<TValue>> @this) =>
            await @this.
                MapTaskAsync(awaitedThis => awaitedThis);
    }
}