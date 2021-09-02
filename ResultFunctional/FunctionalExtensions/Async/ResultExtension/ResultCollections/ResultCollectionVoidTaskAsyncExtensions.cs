using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа с коллекцией задачей-объектом
    /// </summary>
    public static class ResultCollectionVoidTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                     Action<IReadOnlyCollection<TValue>> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionVoidOk(action));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidBadTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                      Action<IReadOnlyCollection<IErrorResult>> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionVoidBad(action));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkBadTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                                     Action<IReadOnlyCollection<TValue>> actionOk,
                                                                                                     Action<IReadOnlyCollection<IErrorResult>> actionBad) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionVoidOkBad(actionOk, actionBad));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ задачи-объекта
        /// </summary>    
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkWhereTaskAsync<TValue>(this Task<IResultCollection<TValue>> @this,
                                                                                          Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                                          Action<IReadOnlyCollection<TValue>> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ResultCollectionVoidOkWhere(predicate, action));
    }
}