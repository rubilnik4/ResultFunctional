using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections.ResultCollectionTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно для задачи-объекта
    /// </summary>
    public static class ResultCollectionTryWhereBindAsyncExtensions
    {
        /// <summary>
        /// Результирующий ответ со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> func,
                                                                                                     Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.
            ResultCollectionBindOkBindAsync(value => ResultCollectionTryAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Результирующий ответ со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkBindAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> func,
                                                                                                     IErrorResult error) =>
            await @this.
            ResultCollectionBindOkBindAsync(value => ResultCollectionTryAsync(() => func.Invoke(value), error));
    }
}