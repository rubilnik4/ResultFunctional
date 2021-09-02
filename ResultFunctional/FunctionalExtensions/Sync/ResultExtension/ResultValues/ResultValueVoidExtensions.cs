using System;
using System.Collections.Generic;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Действие над внутренним типом результирующего ответа со значением
    /// </summary>
    public static class ResultValueVoidExtensions
    {
        /// <summary>
        /// Выполнить действие при положительном значении, вернуть результирующий ответ
        /// </summary>      
        public static IResultValue<TValue> ResultValueVoidOk<TValue>(this IResultValue<TValue> @this, Action<TValue> action) =>
            @this.
            VoidOk(_ => @this.OkStatus,
                action: _ => action.Invoke(@this.Value));

        /// <summary>
        /// Выполнить действие при отрицательном значении, вернуть результирующий ответ
        /// </summary>      
        public static IResultValue<TValue> ResultValueVoidBad<TValue>(this IResultValue<TValue> @this,
                                                                      Action<IReadOnlyCollection<IErrorResult>> action) =>
            @this.
            VoidOk(_ => @this.HasErrors,
                action: _ => action.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие, вернуть результирующий ответ
        /// </summary>      
        public static IResultValue<TValue> ResultValueVoidOkBad<TValue>(this IResultValue<TValue> @this, 
                                                                        Action<TValue> actionOk,
                                                                        Action<IReadOnlyCollection<IErrorResult>> actionBad) =>
            @this.
            VoidWhere(_ => @this.OkStatus,
                actionOk:_ => actionOk.Invoke(@this.Value),
                actionBad: _ => actionBad.Invoke(@this.Errors));

        /// <summary>
        /// Выполнить действие при положительном значении и выполнении условия вернуть результирующий ответ
        /// </summary>    
        public static IResultValue<TValue> ResultValueVoidOkWhere<TValue>(this IResultValue<TValue> @this,
                                                                     Func<TValue, bool> predicate,
                                                                     Action<TValue> action) =>
            @this.
            VoidOk(_ => @this.OkStatus && predicate(@this.Value),
                action: _ => action.Invoke(@this.Value));
    }
}