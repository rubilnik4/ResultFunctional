using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Обработка условий для результирующего ответа задачи-объекта с коллекцией с возвращением к значению
    /// </summary>
    public static class ResultCollectionWhereToValueTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе коллекции с возвращением к значению
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultCollectionContinueToValueTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, TValueOut> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IEnumerable<IErrorResult>> badFunc) =>
            await @this.ToResultValue().
            ResultValueContinueTaskAsync(predicate, okFunc, badFunc);

        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе коллекции с возвращением к значению
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultCollectionOkBadToValueTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, TValueOut> okFunc,
                                                                                                         Func<IReadOnlyCollection<IErrorResult>, TValueOut> badFunc) =>
            await @this.ToResultValue().
            ResultValueOkBadTaskAsync(okFunc, badFunc);

        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки в результирующем ответе коллекции с возвращением к значению
        /// </summary>   
        public static async Task<IResultValue<TValueOut>> ResultCollectionOkToValueTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                                  Func<IReadOnlyCollection<TValueIn>, TValueOut> okFunc) =>
            await @this.ToResultValue().
            ResultValueOkTaskAsync(okFunc);
    }
}