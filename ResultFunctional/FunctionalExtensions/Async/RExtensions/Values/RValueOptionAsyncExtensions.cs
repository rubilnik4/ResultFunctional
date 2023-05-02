using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Extension methods for result value async functor function with conditions
    /// </summary>
    public static class RValueOptionAsyncExtensions
    {
        /// <summary>
        /// Execute result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> RValueOptionAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                            Func<TValueIn, Task<bool>> predicate,
                                                                                            Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                            Func<TValueIn, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await predicate(@this.GetValue())
                    ? await someFunc.Invoke(@this.GetValue()).ToRValueTask()
                    : await noneFunc.Invoke(@this.GetValue()).ToRValueTask<TValueOut>()
                : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> RValueOptionAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                            Func<TValueIn, bool> predicate,
                                                                                            Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                            Func<TValueIn, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.RValueOptionAsync(value => predicate(value).ToTask(), someFunc, noneFunc);

        /// <summary>
        /// Execute result value async function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValueOut>> RValueOptionAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                            Func<TValueIn, bool> predicate,
                                                                                            Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                            Func<TValueIn, IEnumerable<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.RValueOptionAsync(predicate, someFunc,
                                          values => noneFunc(values).ToCollectionTask());

        /// <summary>
        /// Execute result value async function base on predicate condition returning value in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>   
        public static async Task<IRValue<TValueOut>> RValueWhereAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                           Func<TValueIn, bool> predicate,
                                                                                           Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                           Func<TValueIn, Task<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? predicate(@this.GetValue())
                    ? await someFunc.Invoke(@this.GetValue()).ToRValueTask()
                    : await noneFunc.Invoke(@this.GetValue()).ToRValueTask()
                : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute result value async function depending on result value errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <param name="noneFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns> 
        public static async Task<IRValue<TValueOut>> RValueMatchAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                           Func<TValueIn, Task<TValueOut>> someFunc,
                                                                                           Func<IReadOnlyCollection<IRError>, Task<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await someFunc.Invoke(@this.GetValue()).ToRValueTask()
                : await noneFunc.Invoke(@this.GetErrors()).ToRValueTask();

        /// <summary>
        /// Execute result value async function if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if result value hasn't errors</param>
        /// <returns>Outgoing result value</returns>  
        public static async Task<IRValue<TValueOut>> RValueSomeAsync<TValueIn, TValueOut>(this IRValue<TValueIn> @this,
                                                                                          Func<TValueIn, Task<TValueOut>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await someFunc.Invoke(@this.GetValue()).ToRValueTask()
                : @this.GetErrors().ToRValue<TValueOut>();

        /// <summary>
        /// Execute result value async function if incoming result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="noneFunc">Function if result value has errors</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> RValueNoneAsync<TValue>(this IRValue<TValue> @this,
                                                                          Func<IReadOnlyCollection<IRError>, Task<TValue>> noneFunc)
            where TValue : notnull =>
            @this.Success
                ? @this
                : await noneFunc.Invoke(@this.GetErrors()).ToRValueTask();

        /// <summary>
        /// Check errors by predicate async to result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result value</returns>
        public static async Task<IRValue<TValue>> RValueEnsureAsync<TValue>(this IRValue<TValue> @this,
                                                                            Func<TValue, Task<bool>> predicate,
                                                                            Func<TValue, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValue : notnull =>
            await @this.RValueOptionAsync(predicate, Task.FromResult, noneFunc);

        /// <summary>
        /// Check errors by predicate async to result value if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result value</returns>
        public static async Task<IRValue<TValue>> RValueEnsureAsync<TValue>(this IRValue<TValue> @this,
                                                                            Func<TValue, bool> predicate,
                                                                            Func<TValue, Task<IReadOnlyCollection<IRError>>> noneFunc)
            where TValue : notnull =>
            await @this.RValueEnsureAsync(value => predicate(value).ToTask(), noneFunc);
    }
}