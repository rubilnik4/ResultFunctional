using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Обработка асинхронных условий для результирующего ответа с коллекцией
    /// </summary>
    public static class ResultCollectionWhereAsyncExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в асинхронном результирующем ответе с коллекцией
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultCollectionContinueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<IErrorResult>>> badFunc) =>
         @this.OkStatus 
             ? predicate(@this.Value)
                 ? new ResultCollection<TValueOut>(await okFunc.Invoke(@this.Value))
                 : new ResultCollection<TValueOut>(await badFunc.Invoke(@this.Value))
             : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в асинхронном результирующем ответе с коллекцией
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultCollectionWhereAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? new ResultCollection<TValueOut>(await okFunc.Invoke(@this.Value))
                 : new ResultCollection<TValueOut>(await badFunc.Invoke(@this.Value))
             : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение положительного или негативного условия в асинхронном результирующем ответе с коллекцией
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultCollectionOkBadAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                              Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc,
                                                                                              Func<IReadOnlyCollection<IErrorResult>, Task<IEnumerable<TValueOut>>> badFunc) =>
            @this.OkStatus
                ? new ResultCollection<TValueOut>(await okFunc.Invoke(@this.Value))
                : new ResultCollection<TValueOut>(await badFunc.Invoke(@this.Errors));

        /// <summary>
        /// Выполнение положительного условия или возвращение предыдущей ошибки в асинхронном результирующем ответе с коллекцией
        /// </summary>   
        public static async Task<IResultCollection<TValueOut>> ResultCollectionOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this, 
                                                                                           Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<TValueOut>>> okFunc) =>
            @this.OkStatus
                ? new ResultCollection<TValueOut>(await okFunc.Invoke(@this.Value))
                : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение негативного условия или возвращение положительного условия в асинхронном результирующем ответе с коллекцией
        /// </summary>   
        public static async Task<IResultCollection<TValue>> ResultCollectionBadAsync<TValue>(this IResultCollection<TValue> @this,
                                                                            Func<IReadOnlyCollection<IErrorResult>, Task<IEnumerable<TValue>>> badFunc) =>

            @this.OkStatus
                ? @this
                : new ResultCollection<TValue>(await badFunc.Invoke(@this.Errors));
    }
}