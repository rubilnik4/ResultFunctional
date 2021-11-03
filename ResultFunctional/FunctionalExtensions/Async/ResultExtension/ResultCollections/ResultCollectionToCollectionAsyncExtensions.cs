using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Преобразование результирующего ответа в коллекцию асинхронно
    /// </summary>
    public static class ResultCollectionToCollectionAsyncExtensions
    {
        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе и преобразование в значение
        /// </summary>      
        public static async Task<IReadOnlyCollection<TValueOut>> ResultCollectionToCollectionOkBadAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                                            Func<IReadOnlyCollection<IErrorResult>, Task<IEnumerable<TValueOut>>> badFunc) =>
            @this.OkStatus
                ? (await okFunc.Invoke(@this.Value)).ToList()
                : (await badFunc.Invoke(@this.Errors)).ToList();
    }
}