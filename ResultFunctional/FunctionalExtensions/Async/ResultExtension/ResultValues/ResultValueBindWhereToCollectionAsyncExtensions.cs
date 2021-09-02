using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего ответа с значением с возвращением к коллекции
    /// </summary>
    public static class ResultValueBindWhereToCollectionAsyncExtensions
    {
        /// <summary>
        /// Выполнение асинхронного положительного условия или возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>   
        public static async Task<IResultCollection<TValueOut>> ResultValueBindOkToCollectionAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                             Func<TValueIn, Task<IResultCollection<TValueOut>>> okFunc) =>
            await @this.
            ResultValueBindOkAsync(valueIn => okFunc(valueIn).
                                              MapAsync(resultCollection => resultCollection.ToResultValue())).
            ToResultCollectionTaskAsync();
    }
}