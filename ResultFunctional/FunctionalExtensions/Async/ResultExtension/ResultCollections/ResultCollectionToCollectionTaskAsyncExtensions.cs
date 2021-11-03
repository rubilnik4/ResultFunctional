using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Преобразование результирующего ответа в коллекцию для задачи-объекта
    /// </summary>
    public static class ResultCollectionToCollectionTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе и преобразование в значение
        /// </summary>      
        public static async Task<IReadOnlyCollection<TValueOut>> ResultCollectionToCollectionOkBadTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                                                Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc,
                                                                                                                                Func<IReadOnlyCollection<IErrorResult>, IEnumerable<TValueOut>> badFunc) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionToCollectionOkBad(okFunc, badFunc));
    }
}