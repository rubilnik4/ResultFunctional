using System;
using System.Collections.Generic;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections
{
    /// <summary>
    /// Обработка условий для результирующего ответа с коллекцией
    /// </summary>
    public static class ResultCollectionWhereExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в результирующем ответе с коллекцией
        /// </summary>      
        public static IResultCollection<TValueOut> ResultCollectionContinue<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IEnumerable<IErrorResult>> badFunc) =>
         @this.OkStatus 
             ? predicate(@this.Value)
                 ? new ResultCollection<TValueOut>(okFunc.Invoke(@this.Value))
                 : new ResultCollection<TValueOut>(badFunc.Invoke(@this.Value))
             : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе с коллекцией
        /// </summary>      
        public static IResultCollection<TValueOut> ResultCollectionWhere<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? new ResultCollection<TValueOut>(okFunc.Invoke(@this.Value))
                 : new ResultCollection<TValueOut>(badFunc.Invoke(@this.Value))
             : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение положительного или негативного условия в результирующем ответе с коллекцией
        /// </summary>      
        public static IResultCollection<TValueOut> ResultCollectionOkBad<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                              Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc,
                                                                                              Func<IReadOnlyCollection<IErrorResult>, IEnumerable<TValueOut>> badFunc) =>
            @this.OkStatus
                ? new ResultCollection<TValueOut>(okFunc.Invoke(@this.Value))
                : new ResultCollection<TValueOut>(badFunc.Invoke(@this.Errors));

        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки в результирующем ответе с коллекцией
        /// </summary>   
        public static IResultCollection<TValueOut> ResultCollectionOk<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this, 
                                                                                           Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> okFunc) =>
            @this.OkStatus
                ? new ResultCollection<TValueOut>(okFunc.Invoke(@this.Value))
                : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение негативного условия или возвращение положительного условия в результирующем ответе с коллекцией
        /// </summary>   
        public static IResultCollection<TValue> ResultCollectionBad<TValue>(this IResultCollection<TValue> @this,
                                                                            Func<IReadOnlyCollection<IErrorResult>, IEnumerable<TValue>> badFunc) =>

            @this.OkStatus
                ? @this
                : new ResultCollection<TValue>(badFunc.Invoke(@this.Errors));
    }
}