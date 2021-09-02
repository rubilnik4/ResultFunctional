using System;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Обработка условий для результирующего ответа со значением в обертке
    /// </summary>
    public static class ResultValueRawWhereExtensions
    {
        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки в результирующем ответе в обертке
        /// </summary>   
        public static IResultValue<TValueOut> ResultValueRawOk<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<IResultValue<TValueIn>, IResultValue<TValueOut>> okFunc) =>
            @this.OkStatus
                ? okFunc.Invoke(@this)
                : new ResultValue<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение негативного условия результирующего ответа или возвращение положительного в результирующем ответе в обертке
        /// </summary>   
        public static IResultValue<TValue> ResultValueRawBad<TValue>(this IResultValue<TValue> @this,
                                                                     Func<IResultValue<TValue>, IResultValue<TValue>> okFunc) =>
            @this.OkStatus
                ? new ResultValue<TValue>(@this.Value)
                : okFunc.Invoke(@this);
    }
}