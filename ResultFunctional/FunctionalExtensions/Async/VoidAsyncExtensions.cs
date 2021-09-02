using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для асинхронных действий
    /// </summary>
    public static class VoidAsyncExtensions
    {
        /// <summary>
        /// Выполнить асинхронное действие, вернуть тот же тип
        /// </summary>       
        public static async Task<TValue> VoidAsync<TValue>(this TValue @this, Func<TValue, Task> action)
        {
            await action.Invoke(@this);
            return @this;
        }

        /// <summary>
        /// Выполнить асинхронное действие при положительном условии
        /// </summary>
        public static async Task<TValue> VoidOkAsync<TValue>(this TValue @this, Func<TValue, bool> predicate,
                                                             Func<TValue, Task> action) =>
            predicate(@this)
                ? await @this.VoidAsync(_ => action.Invoke(@this))
                : @this;

        /// <summary>
        /// Выполнить асинхронное действие при положительном условии
        /// </summary>
        public static async Task<TValue> VoidWhereAsync<TValue>(this TValue @this, 
                                                                Func<TValue, bool> predicate,
                                                                Func<TValue, Task> actionOk,
                                                                Func<TValue, Task> actionBad) =>
            predicate(@this)
                ? await @this.VoidAsync(_ => actionOk.Invoke(@this))
                : await @this.VoidAsync(_ => actionBad.Invoke(@this));
    }
}