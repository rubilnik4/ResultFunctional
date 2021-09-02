using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Обработка условий для результирующего ответа задачи-объекта с значением с возвращением к коллекции
    /// </summary>
    public static class ResultValueBindWhereToCollectionTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнение асинхронного положительного условия или возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>   
        public static async Task<IResultCollection<TValueOut>> ResultValueBindOkToCollectionTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                             Func<TValueIn, IResultCollection<TValueOut>> okFunc) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.ResultValueBindOkToCollection(okFunc));
    }
}