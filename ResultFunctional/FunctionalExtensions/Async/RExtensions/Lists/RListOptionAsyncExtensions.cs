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
    public static class RListOptionAsyncExtensions
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
        public static async Task<IRList<TValueOut>> RListOptionAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                       Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                       Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                                       Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? await someFunc.Invoke(@this.GetValue()).ToRListTask()
                    : await noneFunc.Invoke(@this.GetValue()).ToRListTask<TValueOut>()
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
        public static async Task<IRList<TValueOut>> RListOptionAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
             await @this.RListOptionAsync(predicate,
                                                       someFunc,
                                                       values => noneFunc(values).ToCollectionTask());

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
        public static async Task<IRList<TValueOut>> RListWhereAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? await someFunc.Invoke(@this.GetValue()).ToRListTask()
                    : await noneFunc.Invoke(@this.GetValue()).ToRListTask()
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
        public static async Task<IRList<TValueOut>> RListWhereAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.RListWhereAsync(predicate,
                                                      someFunc,
                                                      values => noneFunc(values).ToCollectionTask());

        /// <summary>
        /// Execute result collection async function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IRList<TValueOut>> RListMatchAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                              Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                              Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await someFunc.Invoke(@this.GetValue()).ToRListTask()
                : await noneFunc.Invoke(@this.GetErrors()).ToRListTask();

        /// <summary>
        /// Execute result collection async function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RListSomeAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                           Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await someFunc.Invoke(@this.GetValue()).ToRListTask()
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute result collection async function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> RListNoneAsync<TValue>(this IRList<TValue> @this,
                                                                            Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValue>>> noneFunc)
            where TValue : notnull =>
            @this.Success
                ? @this
                : await noneFunc.Invoke(@this.GetErrors()).ToRListTask();

        /// <summary>
        /// Check errors by predicate async to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> RListEnsureAsync<TValue>(this IRList<TValue> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValue : notnull =>
            await @this.
                RListOptionAsync(predicate,
                                              collection => collection.ToCollectionTask(),
                                              noneFunc);

        /// <summary>
        /// Check errors by predicate async to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> RListEnsureAsync<TValue>(this IRList<TValue> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, IReadOnlyCollection<IRError>> noneFunc)
            where TValue : notnull =>
            await @this.RListEnsureAsync(predicate, values => noneFunc(values).ToCollectionTask());
    }
}