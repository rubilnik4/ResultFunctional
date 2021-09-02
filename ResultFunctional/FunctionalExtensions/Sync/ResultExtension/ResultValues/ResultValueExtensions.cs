using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением
    /// </summary>
    public static class ResultValueExtensions
    {
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
        /// Преобразовать значение в результирующий ответ с проверкой на нуль
        /// </summary>
        public static IResultValue<TValue> ToResultValue<TValue>(this TValue @this)
            where TValue : class =>
            new ResultValue<TValue>(@this);

        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль
        /// </summary>
        public static IResultValue<TValue> ToResultValueNullValueCheck<TValue>([AllowNull] this TValue @this, IErrorResult errorNull)
            where TValue : notnull =>
            @this != null
                ? new ResultValue<TValue>(@this)
                : new ResultValue<TValue>(errorNull);

        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль
        /// </summary>
        public static IResultValue<TValue> ToResultValueNullCheck<TValue>(this TValue? @this, IErrorResult errorNull)
            where TValue : class =>
            @this != null
                ? new ResultValue<TValue>(@this)
                : new ResultValue<TValue>(errorNull);

        /// <summary>
        /// Преобразовать значение в результирующий ответ с проверкой на нуль
        /// </summary>
        public static IResultValue<TValue> ToResultValueNullCheck<TValue>(this TValue? @this, IErrorResult errorNull)
            where TValue : struct =>
            @this != null
                ? new ResultValue<TValue>((TValue)@this)
                : new ResultValue<TValue>(errorNull);
    }
}