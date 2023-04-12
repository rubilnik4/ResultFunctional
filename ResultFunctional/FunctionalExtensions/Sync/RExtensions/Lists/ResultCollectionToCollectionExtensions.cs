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
    public static class ResultCollectionToCollectionExtensions
    {
        /// <summary>
        /// Converting result collection to ordinal collection
        /// </summary>      
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if incoming result collection hasn't errors</param>
        /// <param name="badFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing collection</returns>
        public static IReadOnlyCollection<TValueOut> ResultCollectionToCollectionOkBad<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                    Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> okFunc,
                                                                                    Func<IReadOnlyCollection<IRError>, IReadOnlyCollection<TValueOut>> badFunc)
             where TValueIn : notnull
             where TValueOut : notnull =>
            @this.Success
                ? okFunc.Invoke(@this.GetValue()).ToList()
                : badFunc.Invoke(@this.GetErrors()).ToList();
    }
}