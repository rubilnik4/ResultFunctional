using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for task result collection async functor function with conditions
    /// </summary>
    public static class RListOptionAwaitExtensions
    {
        /// <summary>
        /// Execute task result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>        
        public static async Task<IRList<TValueOut>> RListOptionAwait<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                          Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                          Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                          Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RListOptionAsync(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute task result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>        
        public static async Task<IRList<TValueOut>> RListOptionAwait<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                          Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                          Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                          Func<IReadOnlyCollection<TValueIn>, IEnumerable<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.RListOptionAwait(predicate,
                                         someFunc,
                                         values => noneFunc(values).ToCollectionTask());

        /// <summary>
        /// Execute task result collection async function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListWhereAwait<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                         Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RListWhereAsync(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute task result collection async function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListWhereAwait<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                         Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                         Func<IReadOnlyCollection<TValueIn>, IEnumerable<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.RListWhereAwait(predicate,
                                        someFunc,
                                        values => noneFunc(values).ToCollectionTask());

        /// <summary>
        /// Execute task result collection async function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>     
        public static async Task<IRList<TValueOut>> RListMatchAwait<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                         Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RListMatchAsync(someFunc, noneFunc));

        /// <summary>
        /// Execute task result collection async function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListSomeAwait<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                        Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RListSomeAsync(someFunc));

        /// <summary>
        /// Execute task result collection async function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>  
        public static async Task<IRList<TValue>> RListNoneAwait<TValue>(this Task<IRList<TValue>> @this,
                                                                        Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValue>>> noneFunc)
            where TValue : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RListNoneAsync(noneFunc));

        /// <summary>
        /// Check errors by predicate async to task result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> RListEnsureAwait<TValue>(this Task<IRList<TValue>> @this,
                                                                          Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                          Func<IReadOnlyCollection<TValue>, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValue : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RListEnsureAsync(predicate, noneFunc));

        /// <summary>
        /// Check errors by predicate async to task result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> RListEnsureAwait<TValue>(this Task<IRList<TValue>> @this,
                                                                          Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                          Func<IReadOnlyCollection<TValue>, IEnumerable<IRError>> noneFunc)
            where TValue : notnull =>
            await @this.RListEnsureAwait(predicate,
                                         values => noneFunc(values).ToCollectionTask());
    }
}