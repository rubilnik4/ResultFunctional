using System;
using System.Threading.Tasks;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Extension methods for task result value async monad function converting to result collection
    /// </summary>
    public static class RValueBindListOptionAwaitExtensions
    {
        /// <summary>
        /// Execute monad task result value async function converting to result collection if incoming result value hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result value</param>
        /// <param name="someFunc">Function if incoming result value hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static async Task<IRList<TValueOut>> RValueBindListSomeAwait<TValueIn, TValueOut>(this Task<IRValue<TValueIn>> @this,
                                                                                             Func<TValueIn, Task<IRList<TValueOut>>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            await @this.
            MapAwait(thisAwaited => thisAwaited.RValueBindListSomeAsync(someFunc));
    }
}