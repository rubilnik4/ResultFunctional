using System;

namespace ResultFunctional.FunctionalExtensions.Sync
{
    /// <summary>
    /// Extension methods for actions
    /// </summary>
    public static class VoidExtensions
    {
        /// <summary>
        /// Execute action
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged source</returns>
        public static TValue Void<TValue>(this TValue @this, Action<TValue> action)
        {
            action.Invoke(@this);
            return @this;
        }

        /// <summary>
        /// Execute action with predicate
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged source</returns>
        public static TValue VoidOk<TValue>(this TValue @this, Func<TValue, bool> predicate, Action<TValue> action) =>
            predicate(@this)
                ? @this.Void(_ => action.Invoke(@this))
                : @this;

        /// <summary>
        /// Execute action base on predicate condition
        /// </summary>
        /// <typeparam name="TValue">Action type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="actionOk">Action if predicate <see langword="true"/></param>
        /// <param name="actionBad">Action if predicate <see langword="false"/></param>
        /// <returns>Unchanged source</returns>
        public static TValue VoidWhere<TValue>(this TValue @this, Func<TValue, bool> predicate,
                                               Action<TValue> actionOk, Action<TValue> actionBad) =>
            predicate(@this)
                ? @this.Void(_ => actionOk.Invoke(@this))
                : @this.Void(_ => actionBad.Invoke(@this));
    }
}