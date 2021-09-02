using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для проверки условий для задачи-объекта
    /// </summary>
    public static class WhereTaskAsyncExtensions
    {
        /// <summary>
        /// Условие продолжающее действие
        /// </summary>      
        public static async Task<TResult> WhereContinueTaskAsync<TSource, TResult>(this Task<TSource> @this,
                                                                                   Func<TSource, bool> predicate,
                                                                                   Func<TSource, TResult> okFunc,
                                                                                   Func<TSource, TResult> badFunc) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.WhereContinue(predicate, okFunc, badFunc));

        /// <summary>
        /// Обработка позитивного условия
        /// </summary>      
        public static async Task<TSource> WhereOkTaskAsync<TSource>(this Task<TSource> @this,
                                                                  Func<TSource, bool> predicate,
                                                                  Func<TSource, TSource> okFunc) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.WhereOk(predicate, okFunc));

        /// <summary>
        /// Обработка негативного условия
        /// </summary>      
        public static async Task<TSource> WhereBadTaskAsync<TSource>(this Task<TSource> @this,
                                                                     Func<TSource, bool> predicate,
                                                                     Func<TSource, TSource> badFunc) =>
            await @this.
            MapTaskAsync(thisAwaited => thisAwaited.WhereBad(predicate, badFunc));
    }
}