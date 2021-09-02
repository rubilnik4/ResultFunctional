using System;

namespace ResultFunctional.FunctionalExtensions.Sync
{
    /// <summary>
    /// Методы расширения для преобразования типов
    /// </summary>
    public static class MapExtensions
    {
        /// <summary>
        /// Преобразование типов с помощью функции
        /// </summary>       
        public static TResult Map<TSource, TResult>(this TSource @this, Func<TSource, TResult> func) =>
            func(@this);
    }
}
