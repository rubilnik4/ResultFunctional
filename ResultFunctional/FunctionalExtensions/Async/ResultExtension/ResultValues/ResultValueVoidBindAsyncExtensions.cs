using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа со значением задачей-объектом
    /// </summary>
    public static class ResultCollectionVoidBindAsyncExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValue>> ResultValueVoidOkBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                     Func<TValue, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueVoidOkAsync(action));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValue>> ResultValueVoidBadBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                      Func<IReadOnlyCollection<IErrorResult>, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueVoidBadAsync(action));

        /// <summary>
        /// Выполнить действие, вернуть результирующий ответ задачи-объекта
        /// </summary>      
        public static async Task<IResultValue<TValue>> ResultValueVoidOkBadBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                             Func<TValue, Task> actionOk,
                                                                                             Func<IReadOnlyCollection<IErrorResult>, Task> actionBad) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueVoidOkBadAsync(actionOk, actionBad));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ задачи-объекта
        /// </summary>    
        public static async Task<IResultValue<TValue>> ResultValueVoidOkWhereBindAsync<TValue>(this Task<IResultValue<TValue>> @this,
                                                                                          Func<TValue, bool> predicate,
                                                                                          Func<TValue, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ResultValueVoidOkWhereAsync(predicate, action));
    }
}