using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Options;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for result collection monad async function with conditions
    /// </summary>
    public static class ResultCollectionBindWhereAsyncExtensions
    {
        /// <summary>
        /// Execute monad result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindContinueAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                           Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                           Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc,
                                                                                                           Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? await someFunc.Invoke(@this.GetValue())
                    : await noneFunc.Invoke(@this.GetValue()).ToRListTaskAsync<TValueOut>()
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute monad result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindContinueAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                                      Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                                      Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc,
                                                                                                                      Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> noneFunc) 
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultCollectionBindContinueAsync(predicate, someFunc,
                                                          values => noneFunc(values).ToCollectionTask());

        /// <summary>
        /// Execute monad result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindWhereAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                        Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                        Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc,
                                                                                                        Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? await someFunc.Invoke(@this.GetValue())
                    : await noneFunc.Invoke(@this.GetValue())
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute monad result collection async function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindOkBadAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                        Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc,
                                                                                                        Func<IReadOnlyCollection<IRError>, Task<IRList<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await someFunc.Invoke(@this.GetValue())
                : await noneFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Execute monad result collection async function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindOkAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                     Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await someFunc.Invoke(@this.GetValue())
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute monad result collection async function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="noneFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionBindBadAsync<TValue>(this IRList<TValue> @this,
                                                                                      Func<IReadOnlyCollection<IRError>, Task<IRList<TValue>>> noneFunc)
            where TValue : notnull =>
            @this.Success
                ? @this
                : await noneFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Adding errors async to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Error function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionBindErrorsOkAsync<TValue>(this IRList<TValue> @this,
                                                                                           Func<IReadOnlyCollection<TValue>, Task<IROption>> someFunc)
            where TValue : notnull =>
            await @this.
                ResultCollectionBindOkAsync(collection => someFunc.Invoke(collection).ToRListTaskAsync(collection));
    }
}