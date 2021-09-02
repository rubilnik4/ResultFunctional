using System;
using System.Collections.Generic;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа с коллекцией
    /// </summary>
    public static class ResultCollectionBindWhereExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе с коллекцией
        /// </summary>      
        public static IResultCollection<TValueOut> ResultCollectionBindContinue<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> okFunc,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IEnumerable<IErrorResult>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? okFunc.Invoke(@this.Value)
                 : new ResultCollection<TValueOut>(badFunc.Invoke(@this.Value))
             : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе с коллекцией
        /// </summary>      
        public static IResultCollection<TValueOut> ResultCollectionBindWhere<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> okFunc,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? okFunc.Invoke(@this.Value)
                 : badFunc.Invoke(@this.Value)
             : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение положительного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе с коллекцией
        /// </summary>   
        public static IResultCollection<TValueOut> ResultCollectionBindOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                               Func<IReadOnlyCollection<TValueIn>, IResultCollection<TValueOut>> okFunc) =>
            @this.OkStatus
                ? okFunc.Invoke(@this.Value)
                : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение негативного условия результирующего ответа или возвращение положительного в результирующем ответе с коллекцией
        /// </summary>   
        public static IResultCollection<TValue> ResultCollectionBindBad<TValue>(this IResultCollection<TValue> @this,
                                                                                Func<IReadOnlyCollection<IErrorResult>, IResultCollection<TValue>> badFunc) =>
            @this.OkStatus
                ? @this
                : badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Добавить ошибки результирующего ответа или вернуть результат с ошибками для ответа с коллекцией
        /// </summary>
        public static IResultCollection<TValue> ResultCollectionBindErrorsOk<TValue>(this IResultCollection<TValue> @this,
                                                                                     Func<IReadOnlyCollection<TValue>, IResultError> okFunc) =>
            @this.
            ResultCollectionBindOk(collection => okFunc.Invoke(collection).
                                                 Map(resultError => resultError.ToResultCollection(collection)));
    }
}