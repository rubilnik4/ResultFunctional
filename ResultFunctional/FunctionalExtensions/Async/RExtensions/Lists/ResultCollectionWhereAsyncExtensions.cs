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
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IRList<TValueOut>> ResultCollectionContinueAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                       Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                       Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                       Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IRError>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? await okFunc.Invoke(@this.GetValue()).ToRListTaskAsync()
                    : await badFunc.Invoke(@this.GetValue()).ToRListTaskAsync<TValueOut>()
                : @this.GetErrors().ToRList<TValueOut>();


        /// <summary>
        /// Execute result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IRList<TValueOut>> ResultCollectionContinueAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
             await @this.ResultCollectionContinueAsync(predicate,
                                                       okFunc,
                                                       values => badFunc(values).GetCollectionTaskAsync());

        /// <summary>
        /// Execute result collection async function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionWhereAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? await okFunc.Invoke(@this.GetValue()).ToRListTaskAsync()
                    : await badFunc.Invoke(@this.GetValue()).ToRListTaskAsync()
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute result collection async function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionWhereAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                            Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultCollectionWhereAsync(predicate,
                                                      okFunc,
                                                      values => badFunc(values).GetCollectionTaskAsync());

        /// <summary>
        /// Execute result collection async function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>   
        public static async Task<IRList<TValueOut>> ResultCollectionOkBadAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                              Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                              Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await okFunc.Invoke(@this.GetValue()).ToRListTaskAsync()
                : await badFunc.Invoke(@this.GetErrors()).ToRListTaskAsync();

        /// <summary>
        /// Execute result collection async function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionOkAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                           Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await okFunc.Invoke(@this.GetValue()).ToRListTaskAsync()
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute result collection async function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionBadAsync<TValue>(this IRList<TValue> @this,
                                                                            Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValue>>> badFunc)
            where TValue : notnull =>
            @this.Success
                ? @this
                : await badFunc.Invoke(@this.GetErrors()).ToRListTaskAsync();

        /// <summary>
        /// Check errors by predicate async to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionCheckErrorsOkAsync<TValue>(this IRList<TValue> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, Task<IReadOnlyCollection<IRError>>> badFunc)
            where TValue : notnull =>
            await @this.
                ResultCollectionContinueAsync(predicate,
                                              collection => collection.GetCollectionTaskAsync(),
                                              badFunc);

        /// <summary>
        /// Check errors by predicate async to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionCheckErrorsOkAsync<TValue>(this IRList<TValue> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, IReadOnlyCollection<IRError>> badFunc)
            where TValue : notnull =>
            await @this.ResultCollectionCheckErrorsOkAsync(predicate, values => badFunc(values).GetCollectionTaskAsync());
    }
}