using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для преобразования типов для объекта-задачи
    /// </summary>
    public static class MapTaskAsyncExtensions
    {
        /// <summary>
        /// Преобразование типа-задачи с помощью функции
        /// </summary>       
        public static async Task<TResult> MapTaskAsync<TSource, TResult>(this Task<TSource> @this,
                                                                         Func<TSource, TResult> func) =>
            func(await @this);

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        /// <summary>
        /// Преобразование типа-задачи с помощью функции
        /// </summary>       
        public static async ValueTask<TResult> MapValueTaskAsync<TSource, TResult>(this ValueTask<TSource> @this,
                                                                                   Func<TSource, TResult> func) =>
            func(await @this);

        /// <summary>
        /// Преобразование типа-задачи с помощью функции
        /// </summary>       
        public static async Task<TValue> MapValueToTask<TValue>(this ValueTask<TValue> @this) =>
            await @this;
#endif
    }
}