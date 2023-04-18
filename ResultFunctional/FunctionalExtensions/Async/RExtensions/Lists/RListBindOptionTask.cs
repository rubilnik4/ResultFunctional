using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Maybe;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for result collection monad task function with conditions
    /// </summary>
    public static class RListBindOptionTaskExtensions
    {
        /// <summary>
        /// Execute monad result collection task function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns> 
        public static async Task<IRList<TValueOut>> RListBindOptionTask<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                             Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                             Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> someFunc,
                                                                                             Func<IReadOnlyCollection<TValueIn>, IEnumerable<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.RListBindOption(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute monad result collection task function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns> 
        public static async Task<IRList<TValueOut>> RListBindWhereTask<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                            Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> someFunc,
                                                                                            Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.RListBindWhere(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute monad result collection task function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListBindMatchTask<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                            Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> someFunc,
                                                                                            Func<IReadOnlyCollection<IRError>, IRList<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.RListBindMatch(someFunc, noneFunc));

        /// <summary>
        /// Execute monad result collection task function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListBindSomeTask<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                           Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.RListBindSome(someFunc));

        /// <summary>
        /// Execute monad result collection task function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="noneFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing result collection</returns> 
        public static async Task<IRList<TValue>> RListBindNoneTask<TValue>(this Task<IRList<TValue>> @this,
                                                                           Func<IReadOnlyCollection<IRError>, IRList<TValue>> noneFunc)
            where TValue : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.RListBindNone(noneFunc));

        /// <summary>
        /// Adding errors to task result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Error function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> RListBindEnsureTask<TValue>(this Task<IRList<TValue>> @this,
                                                                             Func<IReadOnlyCollection<TValue>, IRMaybe> someFunc)
            where TValue : notnull =>
            await @this.
                MapTask(awaitedThis => awaitedThis.RListBindEnsure(someFunc));
    }
}