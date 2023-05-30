using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Task result value async action extension methods
    /// </summary>
    public static class RValueVoidAwaitExtensions
    {
        /// <summary>
        /// Execute async action if task result value hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns>
        public static async Task<IRValue<TValue>> RValueVoidSomeAwait<TValue>(this Task<IRValue<TValue>> @this,
                                                                              Func<TValue, Task> action)
            where TValue : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RValueVoidSomeAsync(action));

        /// <summary>
        /// Execute async action if task result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns> 
        public static async Task<IRValue<TValue>> RValueVoidNoneAwait<TValue>(this Task<IRValue<TValue>> @this,
                                                                              Func<IReadOnlyCollection<IRError>, Task> action)
            where TValue : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RValueVoidNoneAsync(action));

        /// <summary>
        /// Execute async action depending on task result value errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="actionSome">Action if result value hasn't errors</param>
        /// <param name="actionNone">Action if result value has errors</param>
        /// <returns>Unchanged result value</returns>  
        public static async Task<IRValue<TValue>> RValueVoidMatchAwait<TValue>(this Task<IRValue<TValue>> @this,
                                                                               Func<TValue, Task> actionSome,
                                                                               Action<IReadOnlyCollection<IRError>> actionNone)
            where TValue : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RValueVoidMatchAsync(actionSome, actionNone));

        /// <summary>
        /// Execute async action depending on task result value errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="actionSome">Action if result value hasn't errors</param>
        /// <param name="actionNone">Action if result value has errors</param>
        /// <returns>Unchanged result value</returns>  
        public static async Task<IRValue<TValue>> RValueVoidMatchAwait<TValue>(this Task<IRValue<TValue>> @this,
                                                                               Func<TValue, Task> actionSome,
                                                                               Func<IReadOnlyCollection<IRError>, Task> actionNone)
            where TValue : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RValueVoidMatchAsync(actionSome, actionNone));

        /// <summary>
        /// Execute async action depending on task result value errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result value</returns>   
        public static async Task<IRValue<TValue>> RValueVoidOptionAwait<TValue>(this Task<IRValue<TValue>> @this,
                                                                                Func<TValue, bool> predicate,
                                                                                Func<TValue, Task> action)
            where TValue : notnull =>
            await @this.
                MapAwait(awaitedThis => awaitedThis.RValueVoidOptionAsync(predicate, action));
    }
}