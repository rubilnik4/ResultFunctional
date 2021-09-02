using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Асинхронные методы расширения для результирующего ответа
    /// </summary>
    public static class ResultValueAsyncExtensions
    {
        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckAsync<TValue>(this TValue? @this,
                                                                                           Task<IErrorResult> errorNull)
            where TValue : class =>
            @this.ToResultValueNullCheck(await errorNull);

        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckAsync<TValue>(this TValue? @this,
                                                                                           Task<IErrorResult> errorNull)
            where TValue : struct =>
            @this.ToResultValueNullCheck(await errorNull);
    }
}