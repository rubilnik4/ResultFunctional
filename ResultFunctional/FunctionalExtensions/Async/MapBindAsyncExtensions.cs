using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для асинхронного преобразования типов для объекта-задачи
    /// </summary>
    public static class MapBindAsyncExtensions
    {
        /// <summary>
        /// Преобразование типа-задачи с помощью асинхронной функции
        /// </summary>       
        public static async Task<TResult> MapBindAsync<TSource, TResult>(this Task<TSource> @this, 
                                                                         Func<TSource, Task<TResult>> func) =>
            await func(await @this);
    }
}