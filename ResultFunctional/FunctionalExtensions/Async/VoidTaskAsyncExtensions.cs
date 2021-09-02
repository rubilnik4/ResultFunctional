using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync;

namespace ResultFunctional.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для действий задач-объектов
    /// </summary>
    public static class VoidTaskAsyncExtensions
    {
        /// <summary>
        /// Выполнить действие, вернуть тот же тип
        /// </summary>       
        public static async Task<TValue> VoidTaskAsync<TValue>(this Task<TValue> @this, Action<TValue> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.Void(action));

        /// <summary>
        /// Выполнить действие при положительном условии
        /// </summary>
        public static async Task<TValue> VoidOkTaskAsync<TValue>(this Task<TValue> @this, Func<TValue, bool> predicate,
                                                                 Action<TValue> action) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.VoidOk(predicate, action));

        /// <summary>
        /// Выполнить действие
        /// </summary>
        public static async Task<TValue> VoidWhereTaskAsync<TValue>(this Task<TValue> @this, Func<TValue, bool> predicate,
                                                                    Action<TValue> actionOk, Action<TValue> actionBad) =>
            await @this.
            MapTaskAsync(awaitedThis => awaitedThis.VoidWhere(predicate, actionOk, actionBad));
    }
}