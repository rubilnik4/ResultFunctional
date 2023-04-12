using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for result collection async functor function with conditions
    /// </summary>
    public static class ResultCollectionWhereAsyncExtensions
    {
        /// <summary>
        /// Execute result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IRList<TValueOut>> ResultCollectionContinueAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                       Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                       Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                                       Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? await someFunc.Invoke(@this.GetValue()).ToRListTaskAsync()
                    : await noneFunc.Invoke(@this.GetValue()).ToRListTaskAsync<TValueOut>()
                : @this.GetErrors().ToRList<TValueOut>();


        /// <summary>
        /// Execute result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IRList<TValueOut>> ResultCollectionContinueAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
             await @this.ResultCollectionContinueAsync(predicate,
                                                       someFunc,
                                                       values => noneFunc(values).GetCollectionTaskAsync());

        /// <summary>
        /// Execute result collection async function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionWhereAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? await someFunc.Invoke(@this.GetValue()).ToRListTaskAsync()
                    : await noneFunc.Invoke(@this.GetValue()).ToRListTaskAsync()
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute result collection async function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionWhereAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultCollectionWhereAsync(predicate,
                                                      someFunc,
                                                      values => noneFunc(values).GetCollectionTaskAsync());

        /// <summary>
        /// Execute result collection async function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IRList<TValueOut>> ResultCollectionOkBadAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                              Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                              Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await someFunc.Invoke(@this.GetValue()).ToRListTaskAsync()
                : await noneFunc.Invoke(@this.GetErrors()).ToRListTaskAsync();

        /// <summary>
        /// Execute result collection async function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionOkAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                           Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await someFunc.Invoke(@this.GetValue()).ToRListTaskAsync()
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute result collection async function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionBadAsync<TValue>(this IRList<TValue> @this,
                                                                            Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValue>>> noneFunc)
            where TValue : notnull =>
            @this.Success
                ? @this
                : await noneFunc.Invoke(@this.GetErrors()).ToRListTaskAsync();

        /// <summary>
        /// Check errors by predicate async to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionCheckErrorsOkAsync<TValue>(this IRList<TValue> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValue : notnull =>
            await @this.
                ResultCollectionContinueAsync(predicate,
                                              collection => collection.GetCollectionTaskAsync(),
                                              noneFunc);

        /// <summary>
        /// Check errors by predicate async to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionCheckErrorsOkAsync<TValue>(this IRList<TValue> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, IReadOnlyCollection<IRError>> noneFunc)
            where TValue : notnull =>
            await @this.ResultCollectionCheckErrorsOkAsync(predicate, values => noneFunc(values).GetCollectionTaskAsync());
    }
}