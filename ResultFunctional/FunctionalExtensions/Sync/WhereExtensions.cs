using System;

namespace ResultFunctional.FunctionalExtensions.Sync
{
    /// <summary>
    /// Extension methods for condition functions
    /// </summary>
    public static class WhereTaskExtensions
    {
        /// <summary>
        /// Execute converting function base on predicate condition
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>
        public static TResult WhereContinue<TSource, TResult>(this TSource @this, Func<TSource, bool> predicate,
                                                              Func<TSource, TResult> okFunc, Func<TSource, TResult> badFunc) =>
            predicate(@this)
                ? okFunc.Invoke(@this)
                : badFunc.Invoke(@this);

        /// <summary>
        /// Execute converting function if predicate condition <see langword="true"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <returns>Converting function result</returns>
        public static TSource WhereOk<TSource>(this TSource @this, Func<TSource, bool> predicate, Func<TSource, TSource> okFunc) =>
            @this.WhereContinue(predicate, okFunc, _ => @this);

        /// <summary>
        /// Execute converting function if predicate condition <see langword="false"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>
        public static TSource WhereBad<TSource>(this TSource @this, Func<TSource, bool> predicate, Func<TSource, TSource> badFunc) =>
            @this.WhereContinue(predicate, _ => @this, badFunc);
    }
}