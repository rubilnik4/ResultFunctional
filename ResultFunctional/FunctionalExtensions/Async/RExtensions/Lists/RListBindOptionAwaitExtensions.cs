using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Options;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for result collection monad async task function with conditions
    /// </summary>
    public static class RListBindOptionAwaitExtensions
    {
        /// <summary>
        /// Execute monad result collection async task function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListBindOptionAwait<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                              Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                              Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc,
                                                                                              Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.MapAwait(awaitedThis => awaitedThis.RListBindOptionAsync(predicate, someFunc, noneFunc));
        
        /// <summary>
        /// Execute monad result collection async task function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListBindOptionAwait<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                              Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                              Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc,
                                                                                              Func<IReadOnlyCollection<TValueIn>, IEnumerable<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.RListBindOptionAwait(predicate, someFunc, values => noneFunc(values).ToCollectionTask());

        /// <summary>
        /// Execute monad result collection async task function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListBindOptionWhereAwait<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                   Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                   Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc,
                                                                                                   Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RListBindWhereAsync(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute monad result collection async task function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListBindMatchAwait<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                             Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc,
                                                                                             Func<IReadOnlyCollection<IRError>, Task<IRList<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RListBindMatchAsync(someFunc, noneFunc));

        /// <summary>
        /// Execute monad result collection async task function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListBindSomeAwait<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RListBindSomeAsync(someFunc));

        /// <summary>
        /// Execute monad result collection async task function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="noneFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> RListBindNoneAwait<TValue>(this Task<IRList<TValue>> @this,
                                                                            Func<IReadOnlyCollection<IRError>, Task<IRList<TValue>>> noneFunc)
            where TValue : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RListBindNoneAsync(noneFunc));

        /// <summary>
        /// Adding errors async to task result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Error function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> RListBindEnsureAwait<TValue>(this Task<IRList<TValue>> @this,
                                                                              Func<IReadOnlyCollection<TValue>, Task<IROption>> someFunc)
            where TValue : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RListBindEnsureAsync(someFunc));
    }
}