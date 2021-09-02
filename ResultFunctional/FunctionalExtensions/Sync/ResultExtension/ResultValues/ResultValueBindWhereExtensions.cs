using System;
using System.Collections.Generic;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа со значением
    /// </summary>
    public static class ResultValueBindWhereExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки со связыванием в результирующем ответе
        /// </summary>      
        public static IResultValue<TValueOut> ResultValueBindContinue<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, IResultValue<TValueOut>> okFunc,
                                                                                       Func<TValueIn, IEnumerable<IErrorResult>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? okFunc.Invoke(@this.Value)
                 : new ResultValue<TValueOut>(badFunc.Invoke(@this.Value))
             : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение условия в положительном или негативном варианте со связыванием в результирующем ответе
        /// </summary>      
        public static IResultValue<TValueOut> ResultValueBindWhere<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, IResultValue<TValueOut>> okFunc,
                                                                                       Func<TValueIn, IResultValue<TValueOut>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? okFunc.Invoke(@this.Value)
                 : badFunc.Invoke(@this.Value)
             : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение условия положительного или негативного условия со связыванием в результирующем ответе
        /// </summary>      
        public static IResultValue<TValueOut> ResultValueBindOkBad<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                        Func<TValueIn, IResultValue<TValueOut>> okFunc,
                                                                                        Func<IReadOnlyCollection<IErrorResult>, IResultValue<TValueOut>> badFunc) =>
         @this.OkStatus
             ? okFunc.Invoke(@this.Value)
             : badFunc.Invoke(@this.Errors);


        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static IResultValue<TValueOut> ResultValueBindOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                     Func<TValueIn, IResultValue<TValueOut>> okFunc) =>
            @this.OkStatus
                ? okFunc.Invoke(@this.Value)
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение негативного условия результирующего ответа или возвращение положительного в результирующем ответе
        /// </summary>   
        public static IResultValue<TValue> ResultValueBindBad<TValue>(this IResultValue<TValue> @this,
                                                                      Func<IReadOnlyCollection<IErrorResult>, IResultValue<TValue>> badFunc) =>
            @this.OkStatus
                ? @this
                : badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Добавить ошибки результирующего ответа или вернуть результат с ошибками для ответа со значением
        /// </summary>
        public static IResultValue<TValue> ResultValueBindErrorsOk<TValue>(this IResultValue<TValue> @this,
                                                                           Func<TValue, IResultError> okFunc) =>
            @this.
            ResultValueBindOk(value => okFunc.Invoke(value).
                                       Map(resultError => resultError.ToResultValue(value)));
    }
}