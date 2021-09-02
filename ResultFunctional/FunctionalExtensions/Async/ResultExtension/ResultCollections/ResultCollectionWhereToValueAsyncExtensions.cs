using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего ответа с коллекцией с возвращением к значению
    /// </summary>
    public static class ResultCollectionWhereToValueAsyncExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки асинхронном в результирующем ответе коллекции с возвращением к значению
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultCollectionContinueToValueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<IErrorResult>>> badFunc) =>
            await @this.ToResultValue().
            ResultValueContinueAsync(predicate, okFunc, badFunc);

        /// <summary>
        /// Выполнение положительного или негативного условия асинхронном в результирующем ответе коллекции с возвращением к значению
        /// </summary>      
        public static async Task<IResultValue<TValueOut>> ResultCollectionOkBadToValueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> okFunc,
                                                                                                         Func<IReadOnlyCollection<IErrorResult>, Task<TValueOut>> badFunc) =>
            await @this.ToResultValue().
            ResultValueOkBadAsync(okFunc, badFunc);

        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки асинхронном в результирующем ответе коллекции с возвращением к значению
        /// </summary>   
        public static async Task<IResultValue<TValueOut>> ResultCollectionOkToValueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                                  Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> okFunc) =>
            await @this.ToResultValue().
            ResultValueOkAsync(okFunc);
    }
}