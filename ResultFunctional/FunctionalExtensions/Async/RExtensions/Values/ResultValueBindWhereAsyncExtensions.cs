using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Values
{
    /// <summary>
    /// Extension methods for result value async monad function with conditions
    /// </summary>
    public static class ResultCollectionBindWhereAsyncExtensions
    {
        /// <summary>
        /// Execute monad result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>     
        public static async Task<IRValue<TValueOut>> ResultValueBindContinueAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                                       Func<TValueIn, bool> predicate,
                                                                                                       Func<TValueIn, Task<IRValue<TValueOut>>> okFunc,
                                                                                                       Func<TValueIn, Task<IReadOnlyCollection<IRError>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? await okFunc.Invoke(@this.GetValue())
                    : await badFunc.Invoke(@this.GetValue()).ToRValueTaskAsync<TValueOut>()
                : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute monad result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>     
        public static async Task<IRValue<TValueOut>> ResultValueBindContinueAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                                            Func<TValueIn, bool> predicate,
                                                                                                            Func<TValueIn, Task<IRValue<TValueOut>>> okFunc,
                                                                                                            Func<TValueIn, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ResultValueBindContinueAsync(predicate, okFunc,
                                                     values => badFunc(values).GetCollectionTaskAsync());

        /// <summary>
        /// Execute monad result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>     
        public static async Task<IRValue<TValueOut>> ResultValueBindWhereAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                                         Func<TValueIn, bool> predicate,
                                                                                                         Func<TValueIn, Task<IRValue<TValueOut>>> okFunc,
                                                                                                         Func<TValueIn, Task<IRValue<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? await okFunc.Invoke(@this.GetValue())
                    : await badFunc.Invoke(@this.GetValue())
                : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute monad result value async function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if result value hasn't errors</param>
        /// <param name="badFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> ResultValueBindOkBadAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                                         Func<TValueIn, Task<IRValue<TValueOut>>> okFunc,
                                                                                                         Func<IReadOnlyCollection<IRError>, Task<IRValue<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await okFunc.Invoke(@this.GetValue())
                : await badFunc.Invoke(@this.GetErrors());


        /// <summary>
        /// Execute monad result value async function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> ResultValueBindOkAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                                      Func<TValueIn, Task<IRValue<TValueOut>>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await okFunc.Invoke(@this.GetValue())
                : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute monad result value async function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="badFunc">Function if incoming result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ResultValueBindBadAsync<TValue>(this IRValue<TValue> @this,
                                                                                       Func<IReadOnlyCollection<IRError>, Task<IRValue<TValue>>> badFunc)
            where TValue : notnull =>
            @this.Success
                ? @this
                : await badFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Adding errors async to result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Error function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ResultValueBindErrorsOkAsync<TValue>(this IRValue<TValue> @this,
                                                                                            Func<TValue, Task<IROption>> okFunc)
            where TValue : notnull =>
            await @this.
            ResultValueBindOkAsync(value => okFunc.Invoke(value).ToRValueTaskAsync(value));
    }
}