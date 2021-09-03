using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections.ResultCollectionTryAsyncExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно
    /// </summary>
    public static class ResultCollectionTryWhereAsyncExtensions
    {
        /// <summary>
        /// Результирующий ответ со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> func,
                                                                                                     Func<Exception, IErrorResult> exceptionFunc) =>
            await @this.
            ResultCollectionBindOkAsync(value => ResultCollectionTryAsync(() => func.Invoke(value), exceptionFunc));

        /// <summary>
        /// Результирующий ответ со значением с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> func,
                                                                                                     IErrorResult error) =>
            await @this.
            ResultCollectionBindOkAsync(value => ResultCollectionTryAsync(() => func.Invoke(value), error));
    }
}