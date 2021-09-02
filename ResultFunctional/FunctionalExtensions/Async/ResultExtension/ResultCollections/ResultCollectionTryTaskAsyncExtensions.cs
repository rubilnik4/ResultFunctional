using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultResultFunctional.Models.Interfaces.Results;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections.ResultCollectionTryExtensions;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией и обработкой исключений для задачи-объекта
    /// </summary>
    public static class ResultCollectionTryTaskAsyncExtensions
    {
        /// <summary>
        /// Связать результирующий ответ с коллекцией с обработкой функции при положительном условии для задачи-объекта
        /// </summary>
        public static async Task<IResultCollection<TValueOut>> ResultCollectionTryOkTaskAsync<TValueIn, TValueOut>(this Task<IResultCollection<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> func, 
                                                                                                            IErrorResult error) =>
            await @this.
            ResultCollectionBindOkTaskAsync(value => ResultCollectionTry(() => func.Invoke(value), error));
    }
}