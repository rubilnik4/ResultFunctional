using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего асинхронного связывающего ответа с коллекцией для задачи-объекта
    /// </summary>
    public static class ResultCollectionBindWhereBindAsyncExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе с коллекцией задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindContinueBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<IErrorResult>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindContinueAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе с коллекцией задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindWhereBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindWhereAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Выполнение положительного условия асинхронного результирующего ответа или возвращение предыдущей ошибки в результирующем ответе с коллекцией для задачи-объекта
        /// </summary>   
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindOkBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                      Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> okFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindOkAsync(okFunc));

        /// <summary>
        /// Выполнение негативного условия асинхронного результирующего ответа или возвращение положительного в результирующем ответе с коллекцией для задачи-объекта
        /// </summary>   
        public static async Task<IResultCollection<TValue>> ResultCollectionBindBadBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, Task<IResultCollection<TValue>>> badFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindBadAsync(badFunc));

        /// <summary>
        /// Добавить ошибки асинхронного результирующего ответа или вернуть результат с ошибками для ответа с коллекцией для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValue>> ResultCollectionBindErrorsOkBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<TValue>, Task<IResultError>> okFunc) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionBindErrorsOkAsync(okFunc));


    }
}