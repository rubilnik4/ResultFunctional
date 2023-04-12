using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.Lists
{
    /// <summary>
    /// Result collection async reorder extension methods
    /// </summary>
    public static class ResultCollectionToCollectionAsyncExtensions
    {
        /// <summary>
        /// Async converting result collection to ordinal collection
        /// </summary>      
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if incoming result collection hasn't errors</param>
        /// <param name="badFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing collection</returns>
        public static async Task<IReadOnlyCollection<TValueOut>> ResultCollectionToCollectionOkBadAsync<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                                            Func<IReadOnlyCollection<TValueIn>, Task<IReadOnlyCollection<TValueOut>>> okFunc,
                                                                                                                            Func<IReadOnlyCollection<IRError>, Task<IReadOnlyCollection<TValueOut>>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? await okFunc.Invoke(@this.GetValue())
                : await badFunc.Invoke(@this.GetErrors());
    }
}