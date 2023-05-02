using System;

namespace ResultFunctional.FunctionalExtensions.Sync
{
    /// <summary>
    /// Extension methods for condition functions
    /// </summary>
    public static class OptionExtensions
    {
        /// <summary>
        /// Execute converting function base on predicate condition
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>
        public static TResult Option<TSource, TResult>(this TSource @this, Func<TSource, bool> predicate,
                                                       Func<TSource, TResult> someFunc, Func<TSource, TResult> noneFunc) =>
            predicate(@this)
                ? someFunc.Invoke(@this)
                : noneFunc.Invoke(@this);

        /// <summary>
        /// Execute converting function if predicate condition <see langword="true"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <returns>Converting function result</returns>
        public static TSource OptionSome<TSource>(this TSource @this, Func<TSource, bool> predicate, Func<TSource, TSource> someFunc) =>
            @this.Option(predicate, someFunc, _ => @this);

        /// <summary>
        /// Execute converting function if predicate condition <see langword="false"/>
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <param name="this">Source</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Converting function result</returns>
        public static TSource OptionNone<TSource>(this TSource @this, Func<TSource, bool> predicate, Func<TSource, TSource> noneFunc) =>
            @this.Option(predicate, _ => @this, noneFunc);
    }
}