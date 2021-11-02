using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Преобразование результирующего ответа в значение асинхронно
    /// </summary>
    public static class ResultValueToValueAsyncExtensions
    {
        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе и преобразование в значение
        /// </summary>      
        public static async Task<TValueOut> ResultValueToValueOkBadAsync<TValueIn, TValueOut>(this IResultValue<TValueIn> @this,
                                                                                    Func<TValueIn, Task<TValueOut>> okFunc,
                                                                                    Func<IReadOnlyCollection<IErrorResult>, Task<TValueOut>> badFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke(@this.Value)
                : await badFunc.Invoke(@this.Errors);
    }
}