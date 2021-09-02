using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующей коллекции со значением
    /// </summary>
    public static class ResultCollectionExtensions
    {
        /// <summary>
        /// Преобразовать значение в результирующий ответ коллекции с проверкой на нуль
        /// </summary>
        public static IResultCollection<TValue> ToResultCollectionNullCheck<TValue>(this IEnumerable<TValue?>? @this,
                                                                                    IErrorResult error)
            where TValue : class =>
            @this != null
                ? @this.Select(value => value.ToResultValueNullCheck(error)).
                        ToResultCollection()
                : new ResultCollection<TValue>(error);
    }
}