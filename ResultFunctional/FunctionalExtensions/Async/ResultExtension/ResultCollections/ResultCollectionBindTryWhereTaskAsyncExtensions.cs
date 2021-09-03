using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections.ResultCollectionBindTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно
    /// </summary>
    public static class ResultCollectionBindTryWhereTaskAsyncExtensions
    {
        /// <summary>
        /// Результирующий ответ со связыванием со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValueOut>> ResultValueBindTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                       Func<IEnumerable<TValueIn>, IResultCollection<TValueOut>> func,
                                                                                                       Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.
            ResultCollectionBindOkTaskAsync(value => ResultCollectionBindTry(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Результирующий ответ со связыванием со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValueOut>> ResultValueBindTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                       Func<IEnumerable<TValueIn>, IResultCollection<TValueOut>> func,
                                                                                                       IErrorResult error) =>
            await @this.
            ResultCollectionBindOkTaskAsync(value => ResultCollectionBindTry(() => func.Invoke(value), error));
    }
}