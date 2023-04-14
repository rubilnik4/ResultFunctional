using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Extension methods for result value async functions converting to result value
    /// </summary>
    public static class RValueListOptionAsyncExtensions
    {
        /// <summary>
        /// Execute result value async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>  
        public static async Task<IRList<TValueOut>> RValueListOptionAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                                              Func<TValueIn, bool> predicate,
                                                                                                              Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                                              Func<TValueIn, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            RValueOptionAsync(predicate, someFunc, noneFunc).
            ToRListTaskAsync();

        /// <summary>
        /// Execute result value async function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>  
        public static async Task<IRList<TValueOut>> RValueListOptionAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                                                        Func<TValueIn, bool> predicate,
                                                                                                                        Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                                                        Func<TValueIn, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
             await @this.RValueListOptionAsync(predicate,
                                                              someFunc,
                                                              values => noneFunc(values).ToCollectionTask());

        /// <summary>
        /// Execute result value async function converting to result value depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>     
        public static async Task<IRList<TValueOut>> RValueListMatchAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                    Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
             await @this.RValueListMatchAsync(values => someFunc(values).ToEnumerableTask(),
                                                           values => noneFunc(values).ToEnumerableTask());

        /// <summary>
        /// Execute result value async function converting to result value depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>     
        public static async Task<IRList<TValueOut>> RValueListMatchAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                    Func<TValueIn, Task<IEnumerable<TValueOut>>> someFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, Task<IEnumerable<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            RValueMatchAsync(someFunc, noneFunc).
            ToRListTaskAsync();

        /// <summary>
        /// Execute result value async function converting to result value if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRList<TValueOut>> RValueListSomeAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                             Func<TValueIn, Task<IReadOnlyCollection<TValueOut>>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            RValueSomeAsync(someFunc).
            ToRListTaskAsync();
    }
}