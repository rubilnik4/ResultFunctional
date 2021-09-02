using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа
    /// </summary>
    public static class ResultErrorVoidAsyncExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ResultErrorVoidOkAsync(this IResultError @this, Func<Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.OkStatus,
                action: _ => action.Invoke());

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ResultErrorVoidBadAsync(this IResultError @this,
                                                                       Func<IReadOnlyCollection<IErrorResult>, Task> actionBad) =>
            await @this.
            VoidOkAsync(_ => @this.HasErrors,
                action: _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ResultErrorVoidOkBadAsync(this IResultError @this,
                                                                         Func<Task> actionOk,
                                                                         Func<IReadOnlyCollection<IErrorResult>, Task> actionBad) =>
            await @this.
            VoidWhereAsync(_ => @this.OkStatus,
                actionOk: _ => actionOk.Invoke(),
                actionBad: _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ
        /// </summary>    
        public static async Task<IResultError> ResultErrorVoidOkWhereAsync(this IResultError @this,
                                                                           Func<bool> predicate,
                                                                           Func<Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.OkStatus && predicate(),
                action: _ => action.Invoke());
    }
}