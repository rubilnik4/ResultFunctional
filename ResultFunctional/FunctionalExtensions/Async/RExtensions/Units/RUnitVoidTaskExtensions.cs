﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Units
{
    /// <summary>
    /// Task result error action extension methods
    /// </summary>
    public static class RUnitVoidTaskExtensions
    {
        /// <summary>
        /// Execute action if task result hasn't errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns> 
        public static async Task<IRUnit> RUnitVoidSomeTask(this Task<IRUnit> @this, Action action) =>
            await @this.
            VoidSomeTask(awaitedThis => awaitedThis.Success,
                action: _ => action.Invoke());

        /// <summary>
        /// Execute action if task result has errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="action">Action</param>
        /// <returns>Unchanged result error</returns>
        public static async Task<IRUnit> RUnitVoidNoneTask(this Task<IRUnit> @this,
                                                                       Action<IReadOnlyCollection<IRError>> action) =>
            await @this.
            VoidSomeTask(awaitedThis => awaitedThis.Failure,
                action: awaitedThis => action.Invoke(awaitedThis.GetErrors()));

        /// <summary>
        /// Execute action depending on task result errors
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="actionSome">Action if result hasn't errors</param>
        /// <param name="actionNone">Action if result has errors</param>
        /// <returns>Unchanged result error</returns>   
        public static async Task<IRUnit> RUnitVoidMatchTask(this Task<IRUnit> @this, Action actionSome,
                                                                             Action<IReadOnlyCollection<IRError>> actionNone) =>
            await @this.
            VoidOptionTask(awaitedThis => awaitedThis.Success,
                actionSome: _ => actionSome.Invoke(),
                actionNone: awaitedThis => actionNone.Invoke(awaitedThis.GetErrors()));

        /// <summary>
        /// Execute action depending on task result errors and predicate
        /// </summary>
        /// <param name="this">Incoming result error</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="action">Function if predicate <see langword="true"/></param>
        /// <returns>Unchanged result error</returns>  
        public static async Task<IRUnit> RUnitVoidWhereTask(this Task<IRUnit> @this,
                                                                           Func<bool> predicate,
                                                                           Action action) =>
            await @this.
            VoidSomeTask(awaitedThis=> awaitedThis.Success && predicate(),
                action: _ => action.Invoke());
    }
}