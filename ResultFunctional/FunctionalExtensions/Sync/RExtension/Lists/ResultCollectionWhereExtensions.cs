using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists
{
    /// <summary>
    /// Extension methods for result collection functor function with conditions
    /// </summary>
    public static class ResultCollectionWhereExtensions
    {
        /// <summary>
        /// Execute result collection function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> ResultCollectionContinue<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                      Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                      Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> okFunc,
                                                                                      Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? okFunc.Invoke(@this.GetValue()).ToRList()
                 : badFunc.Invoke(@this.GetValue()).ToRList<TValueOut>()
             : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute result collection function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> ResultCollectionWhere<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> okFunc,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? okFunc.Invoke(@this.GetValue()).ToRList()
                 : badFunc.Invoke(@this.GetValue()).ToRList()
             : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute result collection function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> ResultCollectionOkBad<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                   Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> okFunc,
                                                                                   Func<IReadOnlyCollection<IRError>, IReadOnlyCollection<TValueOut>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? okFunc.Invoke(@this.GetValue()).ToRList()
                : badFunc.Invoke(@this.GetErrors()).ToRList();

        /// <summary>
        /// Execute result collection function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> ResultCollectionOk<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> okFunc)
             where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? okFunc.Invoke(@this.GetValue()).ToRList()
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute result collection function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValue> ResultCollectionBad<TValue>(this IRList<TValue> @this,
                                                                 Func<IReadOnlyCollection<IRError>, IReadOnlyCollection<TValue>> badFunc)
            where TValue : notnull =>
            @this.Success
                ? @this
                : badFunc.Invoke(@this.GetErrors()).ToRList();

        /// <summary>
        /// Check errors by predicate to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static IRList<TValue> ResultCollectionCheckErrorsOk<TValue>(this IRList<TValue> @this,
                                                                           Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                                           Func<IReadOnlyCollection<TValue>, IReadOnlyCollection<IRError>> badFunc)
            where TValue : notnull =>
            @this.
            ResultCollectionContinue(predicate,
                                     value => value,
                                     badFunc);
    }
}