using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Result value async action extension methods
    /// </summary>
    public static class RValueVoidAsyncExtensions
    {
        /// <summary>
        /// Execute async action if result value hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns>
        public static async Task<IRValue<TValue>> RValueVoidSomeAsync<TValue>(this IRValue<TValue> @this,
                                                                              Func<TValue, Task> action)
            where TValue : notnull =>
            await @this.
                VoidSomeAsync(_ => @this.Success,
                              action: _ => action.Invoke(@this.GetValue()));

        /// <summary>
        /// Execute async action if result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns> 
        public static async Task<IRValue<TValue>> RValueVoidNoneAsync<TValue>(this IRValue<TValue> @this,
                                                                              Func<IReadOnlyCollection<IRError>, Task> action)
            where TValue : notnull =>
            await @this.
                VoidSomeAsync(_ => @this.Failure,
                              action: _ => action.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute async action depending on result value errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="actionSome">Action if result value hasn't errors</param>
        /// <param name="actionNone">Action if result value has errors</param>
        /// <returns>Unchanged result value</returns>
        public static async Task<IRValue<TValue>> RValueVoidMatchAsync<TValue>(this IRValue<TValue> @this,
                                                                               Func<TValue, Task> actionSome,
                                                                               Action<IReadOnlyCollection<IRError>> actionNone)
            where TValue : notnull =>
            await @this.
                VoidOptionAsync(_ => @this.Success,
                                _ => actionSome.Invoke(@this.GetValue()),
                                _ => actionNone.ToTask().Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute async action depending on result value errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="actionSome">Action if result value hasn't errors</param>
        /// <param name="actionNone">Action if result value has errors</param>
        /// <returns>Unchanged result value</returns>
        public static async Task<IRValue<TValue>> RValueVoidMatchAsync<TValue>(this IRValue<TValue> @this,
                                                                               Func<TValue, Task> actionSome,
                                                                               Func<IReadOnlyCollection<IRError>, Task> actionNone)
            where TValue : notnull =>
            await @this.
                VoidOptionAsync(_ => @this.Success,
                                _ => actionSome.Invoke(@this.GetValue()),
                                _ => actionNone.Invoke(@this.GetErrors()));

        /// <summary>
        /// Execute async action depending on result value errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result value</returns>   
        public static async Task<IRValue<TValue>> RValueVoidOptionAsync<TValue>(this IRValue<TValue> @this,
                                                                                Func<TValue, bool> predicate,
                                                                                Func<TValue, Task> action)
            where TValue : notnull =>
            await @this.
                VoidSomeAsync(_ => @this.Success && predicate(@this.GetValue()),
                              action: _ => action.Invoke(@this.GetValue()));
    }
}