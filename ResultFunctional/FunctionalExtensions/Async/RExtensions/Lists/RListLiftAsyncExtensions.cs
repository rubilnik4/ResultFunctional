﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists
{
    /// <summary>
    /// Result collection async reorder extension methods
    /// </summary>
    public static class RListLiftAsyncExtensions
    {
        /// <summary>
        /// Async converting result collection to ordinal collection
        /// </summary>      
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if incoming result collection hasn't errors</param>
        /// <param name="noneFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing collection</returns>
        public static async Task<IReadOnlyCollection<TValueOut>> RListLiftMatchAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                          Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                                          Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValueOut>>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await someFunc.Invoke(@this.GetValue())
                : await noneFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Async converting result collection to ordinal collection
        /// </summary>      
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if incoming result collection hasn't errors</param>
        /// <param name="noneFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing collection</returns>
        public static async Task<IReadOnlyCollection<TValueOut>> RListLiftMatchAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                          Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> someFunc,
                                                                                                          Func<IReadOnlyCollection<IRError>, IEnumerable<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this
               .RListLiftMatchAsync(someFunc, values => noneFunc(values).ToCollectionTask());
    }
}