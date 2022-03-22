using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Методы расширения для результирующей коллекции со значением
    /// </summary>
    public static class ResultCollectionExtensions
    {
        /// <summary>
        /// Преобразовать значение в результирующий ответ
        /// </summary>
        public static IResultCollection<TValue> ToResultCollection<TValue>(this IEnumerable<TValue> @this)
            where TValue : notnull =>
            new ResultCollection<TValue>(@this);

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

        /// <summary>
        /// Преобразовать в результирующий ответ со значением в коллекцию
        /// </summary>      
        public static IResultCollection<TValue> ToResultCollection<TValue>(this IResultValue<IEnumerable<TValue>> @this) =>
            @this.OkStatus
                ? new ResultCollection<TValue>(@this.Value)
                : new ResultCollection<TValue>(@this.Errors);

        /// <summary>
        /// Преобразовать в результирующий ответ со значением в коллекцию
        /// </summary>      
        public static IResultCollection<TValue> ToResultCollection<TValue>(this IEnumerable<IResultValue<TValue>> @this) =>
            @this.ToList().
            Map(collection => collection.All(result => result.OkStatus)
                    ? new ResultCollection<TValue>(collection.Select(result => result.Value))
                    : new ResultCollection<TValue>(collection.SelectMany(result => result.Errors)));

        /// <summary>
        /// Преобразовать в результирующий ответ со значением в коллекцию и ошибками
        /// </summary>      
        public static IResultCollection<TValue> ConcatResultCollection<TValue>(this IEnumerable<IResultCollection<TValue>> @this) =>
            ConcatResultCollection(@this.ToList());

        /// <summary>
        /// Преобразовать в результирующий ответ со значением в коллекцию и ошибками
        /// </summary>      
        public static IResultCollection<TValue> ConcatResultCollection<TValue>(this IReadOnlyCollection<IResultCollection<TValue>> @this) =>
            new ResultCollection<TValue>(@this.SelectMany(result => result.Value)).
            ConcatErrors(@this.SelectMany(result => result.Errors));
    }
}