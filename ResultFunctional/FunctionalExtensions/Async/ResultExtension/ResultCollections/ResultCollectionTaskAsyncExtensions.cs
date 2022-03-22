using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией
    /// </summary>
    public static class ResultCollectionTaskAsyncExtensions
    {
       /// <summary>
       /// Преобразовать в ответ со значением-коллекцией
       /// </summary>
        public static async Task<IResultValue<IReadOnlyCollection<TValue>>> ToResultValueTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this) =>
            await @this.MapTaskAsync(awaitedThis => awaitedThis.ToResultValue());

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ToResultErrorTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis);

        /// <summary>
        /// Преобразовать в результирующий ответ со значением в коллекцию и ошибками
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ConcatResultCollectionTaskAsync<TValue>(this Task<IEnumerable<IResultCollection<TValue>>> @this) =>
            await @this.MapTaskAsync(thisAwaited => thisAwaited.ConcatResultCollection());

        /// <summary>
        /// Преобразовать в результирующий ответ со значением в коллекцию и ошибками
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ConcatResultCollectionTaskAsync<TValue>(this Task<IReadOnlyCollection<IResultCollection<TValue>>> @this) =>
            await @this.MapTaskAsync(thisAwaited => thisAwaited.ConcatResultCollection());

        /// <summary>
        /// Преобразовать в результирующий ответ со значением в коллекцию и ошибками
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ConcatResultCollectionTaskAsync<TValue>(this Task<IResultCollection<TValue>[]> @this) =>
            await @this.MapTaskAsync(thisAwaited => thisAwaited.ConcatResultCollection());
    }
}