using System;
using System.Collections.Generic;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Result collection action extension methods
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
                   _ => action.Invoke(@this.Value));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ
        /// </summary>      
        public static IResultCollection<TValue> ResultCollectionVoidBad<TValue>(this IResultCollection<TValue> @this,
                                                                                Action<IReadOnlyCollection<IErrorResult>> action) =>
            @this.
            VoidOk(_ => @this.HasErrors,
                   _ => action.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие, вернуть результирующий ответ
        /// </summary>      
        public static IResultCollection<TValue> ResultCollectionVoidOkBad<TValue>(this IResultCollection<TValue> @this,
                                                                                  Action<IReadOnlyCollection<TValue>> actionOk,
                                                                                  Action<IReadOnlyCollection<IErrorResult>> actionBad) =>
            @this.
            VoidWhere(_ => @this.OkStatus,
                      _ => actionOk.Invoke(@this.Value),
                      _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ
        /// </summary>    
        public static IResultCollection<TValue> ResultCollectionVoidOkWhere<TValue>(this IResultCollection<TValue> @this,
                                                                                    Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                                    Action<IReadOnlyCollection<TValue>> action) =>
            @this.
            VoidOk(_ => @this.OkStatus && predicate(@this.Value),
                   _ => action.Invoke(@this.Value));
    }
}