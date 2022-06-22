using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
{
    /// <summary>
    /// Методы расширения для результирующего ответа задачи-объекта
    /// </summary>
    public static class ResultValueTaskAsyncExtensions
    {
        /// <summary>
        /// Преобразовать в результирующий ответ
        /// </summary>      
        public static async Task<IResultError> ToResultErrorTaskAsync<TValue>(this Task<IResultValue<TValue>> @this) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis);

        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueNullValueCheckTaskAsync<TValue>(this Task<TValue> @this, IErrorResult errorNull)
            where TValue : notnull =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultValueNullValueCheck(errorNull));


        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckTaskAsync<TValue>(this Task<TValue?> @this, IErrorResult errorNull) 
            where TValue : class =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultValueNullCheck(errorNull));

        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль для задачи-объекта
        /// </summary>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckTaskAsync<TValue>(this Task<TValue?> @this, IErrorResult errorNull)
            where TValue : struct =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.ToResultValueNullCheck(errorNull));

        /// <summary>
        /// Преобразовать в результирующий ответ коллекции
        /// </summary>  
        public static async Task<IResultCollection<TValue>> ToResultCollectionTaskAsync<TValue>(this IEnumerable<Task<IResultValue<TValue>>> @this)
            where TValue : notnull =>
            await Task.WhenAll(@this).
            MapTaskAsync(result => result.ToResultCollection());
    }
}