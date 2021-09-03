using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections.ResultCollectionTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений для задачи-объекта
    /// </summary>
    public static class ResultCollectionTryWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Результирующий ответ со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> func,
                                                                                                     Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.
            ResultCollectionBindOkTaskAsync(value => ResultCollectionTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Результирующий ответ со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> func,
                                                                                                     IErrorResult error) =>
            await @this.
            ResultCollectionBindOkTaskAsync(value => ResultCollectionTry(() => func.Invoke(value), error));
    }
}