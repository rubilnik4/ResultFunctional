using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Преобразование результирующего ответа в коллекцию
    /// </summary>
    public static class ResultCollectionToCollectionExtensions
    {
        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе и преобразование в значение
        /// </summary>      
        public static IReadOnlyCollection<TValueOut> ResultCollectionToCollectionOkBad<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                    Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc,
                                                                                    Func<IReadOnlyCollection<IErrorResult>, IEnumerable<TValueOut>> badFunc) =>
            @this.OkStatus
                ? okFunc.Invoke(@this.Value).ToList()
                : badFunc.Invoke(@this.Errors).ToList();
    }
}