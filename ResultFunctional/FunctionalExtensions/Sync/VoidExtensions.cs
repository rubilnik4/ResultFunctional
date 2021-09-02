using System;

namespace ResultFunctional.FunctionalExtensions.Sync
{
    /// <summary>
    /// Методы расширения для действий
    /// </summary>
    public static class VoidExtensions
    {
        /// <summary>
        /// Выполнить действие, вернуть тот же тип
        /// </summary>       
        public static TValue Void<TValue>(this TValue @this, Action<TValue> action)
        {
            action.Invoke(@this);
            return @this;
        }

        /// <summary>
        /// Выполнить действие при положительном условии
        /// </summary>
        public static TValue VoidOk<TValue>(this TValue @this, Func<TValue, bool> predicate, Action<TValue> action) =>
            predicate(@this)
                ? @this.Void(_ => action.Invoke(@this))
                : @this;

        /// <summary>
        /// Выполнить действие
        /// </summary>
        public static TValue VoidWhere<TValue>(this TValue @this, Func<TValue, bool> predicate,
                                               Action<TValue> actionOk, Action<TValue> actionBad) =>
            predicate(@this)
                ? @this.Void(_ => actionOk.Invoke(@this))
                : @this.Void(_ => actionBad.Invoke(@this));
    }
}