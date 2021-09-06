using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections.ResultCollectionBindTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно
    /// </summary>
    public static class ResultCollectionBindTryWhereAsyncExtensions
    {
        /// <summary>
        /// Результирующий ответ со связыванием со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindTryOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                       Func<IEnumerable<TValueIn>, Task<IResultCollection<TValueOut>>> func,
                                                                                                       Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.
            ResultCollectionBindOkAsync(value => ResultCollectionBindTryAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Результирующий ответ со связыванием со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindTryOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                       Func<IEnumerable<TValueIn>, Task<IResultCollection<TValueOut>>> func,
                                                                                                       IErrorResult error) =>
            await @this.
            ResultCollectionBindOkAsync(value => ResultCollectionBindTryAsync(() => func.Invoke(value), error));
    }
}