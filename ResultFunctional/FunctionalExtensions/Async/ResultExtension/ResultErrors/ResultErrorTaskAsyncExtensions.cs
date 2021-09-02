using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Методы расширения для результирующего ответа задачи-объекта
    /// </summary>
    public static class ResultErrorTaskAsyncExtensions
    {
        /// <summary>
        /// Преобразовать в результирующий ответ со значением результирующего ответа
        /// </summary>      
        public static async Task<IResultValue<TValue>> ToResultBindValueTaskAsync<TValue>(this Task<IResultError> @this,
                                                                                          IResultValue<TValue> resultValue) =>
            await @this.
            MapTaskAsync(result => result.ToResultBindValue(resultValue));

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>  
        public static async Task<IResultError> ToResultErrorTaskAsync(this Task<IEnumerable<IResultError>> @this) =>
            await @this.
            MapTaskAsync(result => result.ToResultError());

        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>  
        public static async Task<IResultError> ToResultErrorsTaskAsync(this IEnumerable<Task<IResultError>> @this) =>
            await Task.WhenAll(@this).
            MapTaskAsync(result => result.ToResultError());
    }
}