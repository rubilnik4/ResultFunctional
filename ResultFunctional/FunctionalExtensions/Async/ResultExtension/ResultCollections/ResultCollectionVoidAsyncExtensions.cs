using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Асинхронное действие над внутренним типом результирующего ответа с коллекцией
    /// </summary>
    public static class ResultCollectionVoidAsyncExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkAsync<TValue>(this IResultCollection<TValue> @this,
                                                                                 Func<IReadOnlyCollection<TValue>, Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.OkStatus,
                action: _ => action.Invoke(@this.Value));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidBadAsync<TValue>(this IResultCollection<TValue> @this,
                                                                                  Func<IReadOnlyCollection<IErrorResult>, Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.HasErrors,
                action: _ => action.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ
        /// </summary>      
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkBadAsync<TValue>(this IResultCollection<TValue> @this,
                                                                                                   Func<IReadOnlyCollection<TValue>, Task> actionOk,
                                                                                                   Func<IReadOnlyCollection<IErrorResult>, Task> actionBad) =>
            await @this.
            VoidWhereAsync(_ => @this.OkStatus,
                actionOk: _ => actionOk.Invoke(@this.Value),
                actionBad: _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ
        /// </summary>    
        public static async Task<IResultCollection<TValue>> ResultCollectionVoidOkWhereAsync<TValue>(this IResultCollection<TValue> @this,
                                                                          Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                          Func<IReadOnlyCollection<TValue>, Task> action) =>
            await @this.
            VoidOkAsync(_ => @this.OkStatus && predicate(@this.Value),
                action: _ => action.Invoke(@this.Value));
    }
}