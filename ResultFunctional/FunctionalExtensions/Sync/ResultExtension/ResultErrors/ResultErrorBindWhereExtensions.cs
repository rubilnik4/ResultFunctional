using System;
using System.Collections.Generic;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа
    /// </summary>
    public static class ResultErrorBindWhereAsyncExtensions
    {
        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static IResultError ResultErrorBindOkBad(this IResultError @this, Func<IResultError> okFunc, 
                                                        Func<IReadOnlyCollection<IErrorResult>, IResultError> badFunc) =>
            @this.OkStatus
                ? okFunc.Invoke()
                : badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static IResultError ResultErrorBindOk(this IResultError @this, Func<IResultError> okFunc) =>
            @this.OkStatus
                ? okFunc.Invoke()
                : @this;
    }
}