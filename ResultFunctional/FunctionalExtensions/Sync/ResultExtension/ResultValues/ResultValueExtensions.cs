using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением
    /// </summary>
    public static class ResultValueExtensions
    {
        /// <summary>
        /// Преобразовать значение в результирующий ответ
        /// </summary>
        public static IResultValue<TValue> ToResultValue<TValue>(this TValue @this)
            where TValue : notnull =>
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