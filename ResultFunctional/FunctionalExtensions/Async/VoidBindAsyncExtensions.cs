using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для асинхронных действий и объекта-задачи
    /// </summary>
    public static class VoidBindAsyncExtensions
    {
        /// <summary>
        /// Выполнить асинхронное действие, вернуть тот же тип
        /// </summary>       
        public static async Task<TValue> VoidBindAsync<TValue>(this Task<TValue> @this, Func<TValue, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.VoidAsync(action));
       
        /// <summary>
        /// Выполнить асинхронное действие при положительном условии
        /// </summary>
        public static async Task<TValue> VoidOkBindAsync<TValue>(this Task<TValue> @this, Func<TValue, bool> predicate,
                                                                 Func<TValue, Task> action) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.VoidOkAsync(predicate, action));

        /// <summary>
        /// Выполнить асинхронное действие
        /// </summary>
        public static async Task<TValue> VoidWhereBindAsync<TValue>(this Task<TValue> @this, Func<TValue, bool> predicate,
                                                                 Func<TValue, Task> actionOk, Func<TValue, Task> actionBad) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.VoidWhereAsync(predicate, actionOk, actionBad));
    }
}