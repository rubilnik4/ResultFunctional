using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for result collection monad async functions converting to result value
    /// </summary>
    public static class RListToValueBindOptionAsyncExtensions
    {
        /// <summary>
        /// Execute monad result collection async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>  
        public static async Task<IRValue<TValueOut>> RListToValueBindOptionAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                  Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                  Func<IReadOnlyCollection<TValueIn>, Task<IRValue<TValueOut>>> someFunc,
                                                                                                  Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ToRValue()
               .RValueBindOptionAsync(predicate, someFunc, noneFunc);

        /// <summary>
        /// Execute monad result collection async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>  
        public static async Task<IRValue<TValueOut>> RListToValueBindOptionAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                Func<IReadOnlyCollection<TValueIn>, Task<IRValue<TValueOut>>> someFunc,
                                                                                                Func<IReadOnlyCollection<TValueIn>, IEnumerable<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.RListToValueBindOptionAsync(predicate, someFunc,
                                                    values => noneFunc(values).ToCollectionTask());


        /// <summary>
        /// Execute monad result collection async function converting to result value depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>   
        public static async Task<IRValue<TValueOut>> RListToValueBindMatchAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                               Func<IReadOnlyCollection<TValueIn>, Task<IRValue<TValueOut>>> someFunc,
                                                                                               Func<IReadOnlyCollection<IRError>, Task<IRValue<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ToRValue()
               .RValueBindMatchAsync(someFunc, noneFunc);

        /// <summary>
        /// Execute monad result collection async function converting to result value if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRValue<TValueOut>> RListToValueBindSomeAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                              Func<IReadOnlyCollection<TValueIn>, Task<IRValue<TValueOut>>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ToRValue()
               .RValueBindSomeAsync(someFunc);
    }
}