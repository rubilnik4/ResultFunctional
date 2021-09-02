using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для проверки условий асинхронно для задачи-объекта
    /// </summary>
    public static class WhereBindAsyncExtensions
    {
        /// <summary>
        /// Условие продолжающее действие
        /// </summary>      
        public static async Task<TResult> WhereContinueBindAsync<TSource, TResult>(this Task<TSource> @this,
                                                                                   Func<TSource, bool> predicate,
                                                                                   Func<TSource, Task<TResult>> okFunc,
                                                                                   Func<TSource, Task<TResult>> badFunc) =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.WhereContinueAsync(predicate, okFunc, badFunc));

        /// <summary>
        /// Обработка позитивного условия
        /// </summary>      
        public static async Task<TSource> WhereOkBindAsync<TSource>(this Task<TSource> @this,
                                                                  Func<TSource, bool> predicate,
                                                                  Func<TSource, Task<TSource>> okFunc) =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.WhereOkAsync(predicate, okFunc));

        /// <summary>
        /// Обработка негативного условия
        /// </summary>      
        public static async Task<TSource> WhereBadBindAsync<TSource>(this Task<TSource> @this,
                                                                     Func<TSource, bool> predicate,
                                                                     Func<TSource, Task<TSource>> badFunc) =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.WhereBadAsync(predicate, badFunc));
    }
}