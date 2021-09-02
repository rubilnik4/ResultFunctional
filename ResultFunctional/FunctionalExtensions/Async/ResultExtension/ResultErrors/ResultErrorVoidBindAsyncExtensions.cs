using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа задачи-объекта
    /// </summary>
    public static class ResultErrorVoidBindAsyncExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ResultErrorVoidOkBindAsync(this Task<IResultError> @this, Func<Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.OkStatus,
                action: _ => action.Invoke());

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ResultErrorVoidBadBindAsync(this Task<IResultError> @this,
                                                                       Func<IReadOnlyCollection<IErrorResult>, Task> actionBad) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.HasErrors,
                action: awaitedThis => actionBad.Invoke(awaitedThis.Errors));

        /// <summary>
        /// Выполнить действие, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ResultErrorVoidOkBadBindAsync(this Task<IResultError> @this,
                                                                         Func<Task> actionOk,
                                                                         Func<IReadOnlyCollection<IErrorResult>, Task> actionBad) =>
            await @this.
            VoidWhereBindAsync(awaitedThis => awaitedThis.OkStatus,
                actionOk: _ => actionOk.Invoke(),
                actionBad: awaitedThis => actionBad.Invoke(awaitedThis.Errors));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ
        /// </summary>    
        public static async Task<IResultError> ResultErrorVoidOkWhereBindAsync(this Task<IResultError> @this,
                                                                           Func<bool> predicate,
                                                                           Func<Task> action) =>
            await @this.
            VoidOkBindAsync(awaitedThis => awaitedThis.OkStatus && predicate(),
                action: _ => action.Invoke());
    }
}