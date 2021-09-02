using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего ответа с значением с возвращением к коллекции
    /// </summary>
    public static class ResultValueBindWhereToCollectionBindAsyncExtensions
    {
        /// <summary>
        /// Выполнение асинхронного положительного условия или возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>   
        public static async Task<IResultCollection<TValueOut>> ResultValueBindOkToCollectionBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                             Func<TValueIn, Task<IResultCollection<TValueOut>>> okFunc) =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ResultValueBindOkToCollectionAsync(okFunc));
    }
}