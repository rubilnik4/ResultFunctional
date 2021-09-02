using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа с коллекцией задачей-объектом
    /// </summary>
    public static class ResultCollectionVoidBindAsyncExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                     Func<IReadOnlyCollection<TValue>, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionVoidOkAsync(action));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidBadBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                      Func<IReadOnlyCollection<IErrorResult>, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionVoidBadAsync(action));

        /// <summary>
        /// Выполнить действие, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkBadBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                                     Func<IReadOnlyCollection<TValue>, Task> actionOk,
                                                                                                     Func<IReadOnlyCollection<IErrorResult>, Task> actionBad) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionVoidOkBadAsync(actionOk, actionBad));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ задачи-объекта
        /// </summary>    
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkWhereBindAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                          Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                                          Func<IReadOnlyCollection<TValue>, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultCollectionVoidOkWhereAsync(predicate, action));
    }
}