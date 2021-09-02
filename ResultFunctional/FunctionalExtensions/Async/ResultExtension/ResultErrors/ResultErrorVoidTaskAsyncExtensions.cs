using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа задачи-объекта
    /// </summary>
    public static class ResultErrorVoidTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ResultErrorVoidOkTaskAsync(this Task<IResultError> @this, Action action) =>
            await @this.
            VoidOkTaskAsync(awaitedThis => awaitedThis.OkStatus,
                action: _ => action.Invoke());

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ResultErrorVoidBadTaskAsync(this Task<IResultError> @this,
                                                                       Action<IReadOnlyCollection<IErrorResult>> actionBad) =>
            await @this.
            VoidOkTaskAsync(awaitedThis => awaitedThis.HasErrors,
                action: awaitedThis => actionBad.Invoke(awaitedThis.Errors));

        /// <summary>
        /// Выполнить действие, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ResultErrorVoidOkBadTaskAsync(this Task<IResultError> @this,
                                                                         Action actionOk,
                                                                         Action<IReadOnlyCollection<IErrorResult>> actionBad) =>
            await @this.
            VoidWhereTaskAsync(awaitedThis => awaitedThis.OkStatus,
                actionOk: _ => actionOk.Invoke(),
                actionBad: awaitedThis => actionBad.Invoke(awaitedThis.Errors));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ
        /// </summary>    
        public static async Task<IResultError> ResultErrorVoidOkWhereTaskAsync(this Task<IResultError> @this,
                                                                           Func<bool> predicate,
                                                                           Action action) =>
            await @this.
            VoidOkTaskAsync(awaitedThis=> awaitedThis.OkStatus && predicate(),
                action: _ => action.Invoke());
    }
}