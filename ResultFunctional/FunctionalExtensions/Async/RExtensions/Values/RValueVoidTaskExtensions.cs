using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Task result value action extension methods
    /// </summary>
    public static class RValueVoidTaskExtensions
    {
        /// <summary>
        /// Execute action if task result value hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns>
        public static async Task<IRValue<TValue>> RValueVoidSomeTask<TValue>(this Task<IRValue<TValue>> @this,
                                                                                     Action<TValue> action)
            where TValue : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RValueVoidSome(action));

        /// <summary>
        /// Execute action if task result value has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result value</returns>      
        public static async Task<IRValue<TValue>> RValueVoidNoneTask<TValue>(this Task<IRValue<TValue>> @this,
                                                                                      Action<IReadOnlyCollection<IRError>> action)
            where TValue : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RValueVoidNone(action));

        /// <summary>
        /// Execute action depending on task result value errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="actionSome">Action if result value hasn't errors</param>
        /// <param name="actionNone">Action if result value has errors</param>
        /// <returns>Unchanged result value</returns>  
        public static async Task<IRValue<TValue>> RValueVoidMatchTask<TValue>(this Task<IRValue<TValue>> @this,
                                                                                           Action<TValue> actionSome,
                                                                                           Action<IReadOnlyCollection<IRError>> actionNone)
            where TValue : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RValueVoidMatch(actionSome, actionNone));

        /// <summary>
        /// Execute action depending on task result value errors and predicate
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result value</returns>   
        public static async Task<IRValue<TValue>> RValueVoidOptionTask<TValue>(this Task<IRValue<TValue>> @this,
                                                                                          Func<TValue, bool> predicate,
                                                                                          Action<TValue> action)
            where TValue : notnull =>
            await @this.
            MapTask(awaitedThis => awaitedThis.RValueVoidOption(predicate, action));
    }
}