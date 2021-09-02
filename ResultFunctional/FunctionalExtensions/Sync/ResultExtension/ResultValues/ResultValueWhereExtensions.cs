using System;
using System.Collections.Generic;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Обработка условий для результирующего ответа со значением
    /// </summary>
    public static class ResultValueWhereExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>      
        public static IResultValue<TValueOut> ResultValueContinue<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, TValueOut> okFunc,
                                                                                       Func<TValueIn, IEnumerable<IErrorResult>> badFunc) =>
         @this.OkStatus 
             ? predicate(@this.Value)
                 ? new ResultValue<TValueOut>(okFunc.Invoke(@this.Value))
                 : new ResultValue<TValueOut>(badFunc.Invoke(@this.Value))
             : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение условия в положительном или негативном варианте в результирующем ответе
        /// </summary>      
        public static IResultValue<TValueOut> ResultValueWhere<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                       Func<TValueIn, bool> predicate,
                                                                                       Func<TValueIn, TValueOut> okFunc,
                                                                                       Func<TValueIn, TValueOut> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? new ResultValue<TValueOut>(okFunc.Invoke(@this.Value))
                 : new ResultValue<TValueOut>(badFunc.Invoke(@this.Value))
             : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе
        /// </summary>      
        public static IResultValue<TValueOut> ResultValueOkBad<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, TValueOut> okFunc,
                                                                                    Func<IReadOnlyCollection<IErrorResult>, TValueOut> badFunc) =>
            @this.OkStatus
                ? new ResultValue<TValueOut>(okFunc.Invoke(@this.Value))
                : new ResultValue<TValueOut>(badFunc.Invoke(@this.Errors));

        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        public static IResultValue<TValueOut> ResultValueOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this, 
                                                                                 Func<TValueIn, TValueOut> okFunc) =>
            @this.OkStatus
                ? new ResultValue<TValueOut>(okFunc.Invoke(@this.Value))
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение негативного условия или возвращение положительного условия в результирующем ответе
        /// </summary>   
        public static IResultValue<TValue> ResultValueBad<TValue>(this IResultValue<TValue> @this,
                                                                  Func<IReadOnlyCollection<IErrorResult>, TValue> badFunc) =>

            @this.OkStatus
                ? @this
                : new ResultValue<TValue>(badFunc.Invoke(@this.Errors));
    }
}