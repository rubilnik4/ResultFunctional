using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections
{
    /// <summary>
    /// Обработка условий для результирующего асинхронного связывающего ответа с коллекцией
    /// </summary>
    public static class ResultCollectionBindWhereAsyncExtensions
    {
        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в асинхронном результирующем ответе с коллекцией
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindContinueAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IEnumerable<IErrorResult>>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? await okFunc.Invoke(@this.Value)
                 : new ResultCollection<TValueOut>(await badFunc.Invoke(@this.Value))
             : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение условия или возвращение предыдущей ошибки в асинхронном результирующем ответе с коллекцией
        /// </summary>      
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindWhereAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> badFunc) =>
         @this.OkStatus
             ? predicate(@this.Value)
                 ? await okFunc.Invoke(@this.Value)
                 : await badFunc.Invoke(@this.Value)
             : new ResultCollection<TValueOut>(@this.Errors);

        /// <summary>
        /// Выполнение положительного условия результирующего асинхронного ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе с коллекцией
        /// </summary>   
        public static async Task<IResultCollection<TValueOut>> ResultCollectionBindOkAsync<TValueIn, TValueOut>(this IResultCollection<TValueIn> @this,
                                                                                                      Func<IReadOnlyCollection<TValueIn>, Task<IResultCollection<TValueOut>>> okFunc) =>
            @this.OkStatus
                ? await okFunc.Invoke(@this.Value)
                : new ResultCollection<TValueOut>(@this.Errors);


        /// <summary>
        /// Выполнение негативного условия результирующего асинхронного ответа или возвращение положительного в результирующем ответе с коллекцией
        /// </summary>   
        public static async Task<IResultCollection<TValue>> ResultCollectionBindBadAsync<TValue>(this IResultCollection<TValue> @this,
                                                                                       Func<IReadOnlyCollection<IErrorResult>, Task<IResultCollection<TValue>>> badFunc) =>
            @this.OkStatus
                ? @this
                : await badFunc.Invoke(@this.Errors);

        /// <summary>
        /// Добавить асинхронно ошибки результирующего ответа или вернуть результат с ошибками для ответа с коллекцией
        /// </summary>
        public static async Task<IResultCollection<TValue>> ResultCollectionBindErrorsOkAsync<TValue>(this IResultCollection<TValue> @this,
                                                                                       Func<IReadOnlyCollection<TValue>, Task<IResultError>> okFunc) =>
            await @this.
            ResultCollectionBindOkAsync(collection => okFunc.Invoke(collection).
                                                      MapTaskAsync(resultError => resultError.ToResultCollection(collection)));


    }
}