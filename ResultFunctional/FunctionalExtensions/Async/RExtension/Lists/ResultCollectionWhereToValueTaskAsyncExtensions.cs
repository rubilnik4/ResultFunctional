using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Lists
{
    /// <summary>
    /// Extension methods for result collection task functions converting to result value
    /// </summary>
    public static class ResultCollectionWhereToValueTaskAsyncExtensions
    {
        /// <summary>
        /// Execute result collection task function converting to result value base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>     
        public static async Task<IRValue<TValueOut>> ResultCollectionContinueToValueTaskAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                                   Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                                   Func<IReadOnlyCollection<TValueIn>, TValueOut> okFunc,
                                                                                                                   Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ToRValueFromCollectionTaskAsync().
            ResultValueContinueTaskAsync(predicate, okFunc, badFunc);

        /// <summary>
        /// Execute result collection task function converting to result value depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result value</returns>        
        public static async Task<IRValue<TValueOut>> ResultCollectionOkBadToValueTaskAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                         Func<IReadOnlyCollection<TValueIn>, TValueOut> okFunc,
                                                                                                         Func<IReadOnlyCollection<IRError>, TValueOut> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ToRValueFromCollectionTaskAsync().
            ResultValueOkBadTaskAsync(okFunc, badFunc);

        /// <summary>
        /// Execute result collection task function converting to result value if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns> 
        public static async Task<IRValue<TValueOut>> ResultCollectionOkToValueTaskAsync<TValueIn, TValueOut>(this Task<IRList<TValueIn>> @this,
                                                                                                                  Func<IReadOnlyCollection<TValueIn>, TValueOut> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.ToRValueFromCollectionTaskAsync().
            ResultValueOkTaskAsync(okFunc);
    }
}