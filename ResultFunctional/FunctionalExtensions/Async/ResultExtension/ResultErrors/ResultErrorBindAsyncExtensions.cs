using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors
{
    /// <summary>
    /// Асинхронные методы расширения для результирующего ответа задачи-объекта
    /// </summary>
    public static class ResultErrorBindAsyncExtensions
    {
        /// <summary>
        /// Преобразовать в результирующий ответ со значением результирующего ответа асинхронно
        /// </summary>      
        public static async Task<IResultValue<TValue>> ToResultBindValueBindAsync<TValue>(this Task<IResultError> @this, Task<IResultValue<TValue>> resultValue) =>
            await @this.
            MapBindAsync(result => result.ToResultBindValueAsync(resultValue));
    }
}