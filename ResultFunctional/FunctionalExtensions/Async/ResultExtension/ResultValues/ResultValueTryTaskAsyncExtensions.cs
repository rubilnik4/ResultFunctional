using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues.ResultValueTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений для задачи-объекта
    /// </summary>
    public static class ResultValueTryTaskAsyncExtensions
    {
        /// <summary>
        /// Связать результирующий ответ со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValueOut>> ResultValueTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                                         Func<TValueIn, TValueOut> func, IErrorResult error) =>
            await @this.
            ResultValueBindOkTaskAsync(value => ResultValueTry(() => func.Invoke(value), error));
    }
}