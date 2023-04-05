using System;
using System.Threading.Tasks;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues
{
    /// <summary>
    /// Extension methods for task result value async monad function converting to result collection
    /// </summary>
    public static class ResultValueBindWhereToCollectionBindAsyncExtensions
    {
        /// <summary>
        /// Execute monad task result value async function converting to result collection if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="okFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IResultCollection<TValueOut>> ResultValueBindOkToCollectionBindAsync<TValueIn, TValueOut>(this Task<IResultValue<TValueIn>> @this,
                                                                                             Func<TValueIn, Task<IResultCollection<TValueOut>>> okFunc)
            where TValueOut : notnull =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ResultValueBindOkToCollectionAsync(okFunc));
    }
}