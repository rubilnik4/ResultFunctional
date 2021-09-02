using System;
using System.Collections.Generic;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Преобразование значений в результирующий ответ коллекции с условием
    /// </summary>
    public static class ToResultCollectionWhereExtensions
    {
        /// <summary>
        /// Преобразовать значений в результирующий ответ коллекции с условием
        /// </summary>
        public static IResultCollection<TValue> ToResultCollectionWhere<TValue>(this IEnumerable<TValue> @this,
                                                                                Func<IEnumerable<TValue>, bool> predicate,
                                                                                Func<IEnumerable<TValue>, IErrorResult> badFunc)
            where TValue : notnull =>
          @this.WhereContinue(predicate,
                              values => new ResultCollection<TValue>(values),
                              values => new ResultCollection<TValue>(badFunc(values)));
    }
}