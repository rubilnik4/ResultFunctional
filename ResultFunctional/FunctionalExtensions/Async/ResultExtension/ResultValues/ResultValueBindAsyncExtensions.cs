using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Асинхронные методы расширения для результирующего ответа для задачи-объекта
    /// </summary>
    public static class ResultValueBindAsyncExtensions
    {
        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckBindAsync<TValue>(this Task<TValue?> @this,
                                                                                           Task<IErrorResult> errorNull)
            where TValue : class =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueNullCheckAsync(errorNull));

        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckBindAsync<TValue>(this Task<TValue?> @this,
                                                                                               Task<IErrorResult> errorNull)
            where TValue : struct =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueNullCheckAsync(errorNull));
    }
}