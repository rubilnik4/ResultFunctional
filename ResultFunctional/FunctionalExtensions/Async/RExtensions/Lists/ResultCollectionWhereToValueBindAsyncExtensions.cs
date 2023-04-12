using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for task result collection async functions converting to result value
    /// </summary>
    public static class ResultCollectionWhereToValueBindAsyncExtensions
    {
        /// <summary>
        /// Execute result collection task async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>     
        public static async Task<IRValue<TValueOut>> ResultCollectionContinueToValueBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                                   Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                                   Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> someFunc,
                                                                                                                   Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
                MapBindAsync(thisAwaited => thisAwaited.ResultCollectionContinueToValueAsync(predicate, someFunc, noneFunc));

        /// <summary>
        /// Execute result collection task async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>     
        public static async Task<IRValue<TValueOut>> ResultCollectionContinueToValueBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> someFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultCollectionContinueToValueBindAsync(predicate, someFunc,
                                                      values => noneFunc(values).GetCollectionTaskAsync());

        /// <summary>
        /// Execute result collection task async function converting to result value depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>   
        public static async Task<IRValue<TValueOut>> ResultCollectionOkBadToValueBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> someFunc,
                                                                                                         Func<IReadOnlyCollection<IRError>, Task<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ResultCollectionOkBadToValueAsync(someFunc, noneFunc));

        /// <summary>
        /// Execute result collection task async function converting to result value if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns> 
        public static async Task<IRValue<TValueOut>> ResultCollectionOkToValueBindAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                                  Func<IReadOnlyCollection<TValueIn>, Task<TValueOut>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ResultCollectionOkToValueAsync(someFunc));
    }
}