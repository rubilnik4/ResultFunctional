using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для асинхронного преобразования типов
    /// </summary>
    public static class MapAsyncExtensions
    {
        /// <summary>
        /// Асинхронное преобразование типов с помощью функции
        /// </summary>       
        public static async Task<TResult> MapAsync<TSource, TResult>(this TSource @this, Func<TSource, Task<TResult>> func) =>
            await func(@this);
    }
}