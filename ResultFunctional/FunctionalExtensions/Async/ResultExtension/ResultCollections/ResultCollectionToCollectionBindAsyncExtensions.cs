using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Преобразование результирующего ответа в коллекцию для задачи-объекта асинхронно
    /// </summary>
    public static class ResultCollectionToCollectionBindAsyncExtensions
    {
        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе и преобразование в значение
        /// </summary>      
        public static async Task<IReadOnlyCollection<TValueOut>> ResultCollectionToCollectionOkBadBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                                                Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                                                Func<IReadOnlyCollection<IErrorResult>, Task<IEnumerable<TValueOut>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionToCollectionOkBadAsync(okFunc, badFunc));
    }
}