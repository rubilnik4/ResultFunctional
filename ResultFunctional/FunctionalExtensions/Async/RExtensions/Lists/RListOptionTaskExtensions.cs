using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for task result collection functor function with conditions
    /// </summary>
    public static class RListOptionTaskExtensions
    {
        /// <summary>
        /// Execute task result collection function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>           
        public static async Task<IRList<TValueOut>> RListOptionTask<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                           Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                           Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> someFunc,
                                                                                                           Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RListOption(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute task result collection function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListWhereTask<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> someFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RListWhere(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute task result collection function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>     
        public static async Task<IRList<TValueOut>> RListMatchTask<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> someFunc,
                                                                                                         Func<IReadOnlyCollection<IRError>, IReadOnlyCollection<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RListMatch(someFunc, noneFunc));

        /// <summary>
        /// Execute task result collection function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListSomeTask<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                      Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RListSome(someFunc));

        /// <summary>
        /// Execute task result collection function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>  
        public static async Task<IRList<TValue>> RListNoneTask<TValue>(this Task<IRList<TValue>> @this,
                                                                                       Func<IReadOnlyCollection<IRError>, IReadOnlyCollection<TValue>> noneFunc)
            where TValue : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RListNone(noneFunc));

        /// <summary>
        /// Check errors by predicate to task result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> RListEnsureTask<TValue>(this Task<IRList<TValue>> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, IReadOnlyCollection<IRError>> noneFunc)
            where TValue : notnull =>
             await @this.
             MapTask(awaitedThis => awaitedThis.RListEnsure(predicate, noneFunc));
    }
}