using System;
using System.Collections.Generic;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа с коллекцией
    /// </summary>
    public static class ResultCollectionVoidExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ
        /// </summary>      
        public static IResultCollection<TValue> ResultCollectionVoidOk<TValue>(this IResultCollection<TValue> @this, 
                                                                               Action<IReadOnlyCollection<TValue>> action) =>
            @this.
            VoidOk(_ => @this.OkStatus,
                action: _ => action.Invoke(@this.Value));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ
        /// </summary>      
        public static IResultCollection<TValue> ResultCollectionVoidBad<TValue>(this IResultCollection<TValue> @this,
                                                                                Action<IReadOnlyCollection<IErrorResult>> action) =>
            @this.
            VoidOk(_ => @this.HasErrors,
                action: _ => action.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие, вернуть результирующий ответ
        /// </summary>      
        public static IResultCollection<TValue> ResultCollectionVoidOkBad<TValue>(this IResultCollection<TValue> @this,
                                                                                  Action<IReadOnlyCollection<TValue>> actionOk,
                                                                                  Action<IReadOnlyCollection<IErrorResult>> actionBad) =>
            @this.
            VoidWhere(_ => @this.OkStatus,
                actionOk: _ => actionOk.Invoke(@this.Value),
                actionBad: _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ
        /// </summary>    
        public static IResultCollection<TValue> ResultCollectionVoidOkWhere<TValue>(this IResultCollection<TValue> @this,
                                                                                    Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                                    Action<IReadOnlyCollection<TValue>> action) =>
            @this.
            VoidOk(_ => @this.OkStatus && predicate(@this.Value),
                action: _ => action.Invoke(@this.Value));
    }
}