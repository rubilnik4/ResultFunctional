using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists
{
    /// <summary>
    /// Result collection reorder extension methods
    /// </summary>
    public static class RListLiftExtensions
    {
        /// <summary>
        /// Converting result collection to ordinal collection
        /// </summary>      
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if incoming result collection hasn't errors</param>
        /// <param name="noneFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing collection</returns>
        public static IReadOnlyCollection<TValueOut> RListLiftMatch<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                         Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> someFunc,
                                                                                         Func<IReadOnlyCollection<IRError>, IEnumerable<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? someFunc.Invoke(@this.GetValue()).ToList()
                : noneFunc.Invoke(@this.GetErrors()).ToList();
    }
}