using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для проверки условий асинхронно
    /// </summary>
    public static class WhereAsyncExtensions
    {
        /// <summary>
        /// Условие продолжающее действие
        /// </summary>      
        public static async Task<TResult> WhereContinueAsync<TSource, TResult>(this TSource @this,
                                                                              Func<TSource, bool> predicate,
                                                                              Func<TSource, Task<TResult>> okFunc,
                                                                              Func<TSource, Task<TResult>> badFunc) =>
             predicate(@this)
                ? await okFunc.Invoke(@this)
                : await badFunc.Invoke(@this);

        /// <summary>
        /// Обработка позитивного условия
        /// </summary>      
        public static async Task<TSource> WhereOkAsync<TSource>(this TSource @this,
                                                               Func<TSource, bool> predicate,
                                                               Func<TSource, Task<TSource>> okFunc) =>
             await @this.WhereContinueAsync(predicate, okFunc, _ => Task.FromResult(@this));

        /// <summary>
        /// Обработка негативного условия
        /// </summary>      
        public static async Task<TSource> WhereBadAsync<TSource>(this TSource @this,
                                                                Func<TSource, bool> predicate,
                                                                Func<TSource, Task<TSource>> badFunc) =>
            await @this.WhereContinueAsync(predicate, _ => Task.FromResult(@this), badFunc);
    }
}