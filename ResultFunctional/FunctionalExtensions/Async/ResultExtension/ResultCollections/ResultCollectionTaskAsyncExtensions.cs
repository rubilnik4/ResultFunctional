using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

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
        public static async Task<IResultValue<IReadOnlyCollection<TValue>>> ToResultValue<TValue>(this Task<IResultCollection<TValue>> @this) =>
            await @this.MapTaskAsync(awaitedThis => awaitedThis.ToResultValue());

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ToResultErrorTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis);
    }
}