using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Lists
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
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindContinueAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                           Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                           Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> okFunc,
                                                                                                           Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<IRError>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? await okFunc.Invoke(@this.GetValue())
                    : await badFunc.Invoke(@this.GetValue()).ToRListTaskAsync<TValueOut>()
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute monad result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindContinueAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                                      Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                                      Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> okFunc,
                                                                                                                      Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> badFunc) 
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultCollectionBindContinueAsync(predicate, okFunc,
                                                          values => badFunc(values).GetCollectionTaskAsync());

        /// <summary>
        /// Execute monad result collection async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindWhereAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                        Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                        Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> okFunc,
                                                                                                        Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? await okFunc.Invoke(@this.GetValue())
                    : await badFunc.Invoke(@this.GetValue())
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute monad result collection async function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindOkBadAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                        Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> okFunc,
                                                                                                        Func<IReadOnlyCollection<IRError>, Task<IRList<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await okFunc.Invoke(@this.GetValue())
                : await badFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Execute monad result collection async function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> ResultCollectionBindOkAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                                Func<IReadOnlyCollection<TValueIn>, Task<IRList<TValueOut>>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await okFunc.Invoke(@this.GetValue())
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute monad result collection async function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionBindBadAsync<TValue>(this IRList<TValue> @this,
                                                                                      Func<IReadOnlyCollection<IRError>, Task<IRList<TValue>>> badFunc)
            where TValue : notnull =>
            @this.Success
                ? @this
                : await badFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Adding errors async to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Error function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValue>> ResultCollectionBindErrorsOkAsync<TValue>(this IRList<TValue> @this,
                                                                                           Func<IReadOnlyCollection<TValue>, Task<IRUnit>> okFunc)
            where TValue : notnull =>
            await @this.
                ResultCollectionBindOkAsync(collection => okFunc.Invoke(collection).ToRListTaskAsync(collection));
    }
}