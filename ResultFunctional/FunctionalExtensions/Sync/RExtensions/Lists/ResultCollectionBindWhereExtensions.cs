using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Options;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for result collection monad function with conditions
    /// </summary>
    public static class ResultCollectionBindWhereExtensions
    {
        /// <summary>
        /// Execute monad result collection function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> ResultCollectionBindContinue<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                          Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                          Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> okFunc,
                                                                                          Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? okFunc.Invoke(@this.GetValue())
                 : badFunc.Invoke(@this.GetValue()).ToRList<TValueOut>()
             : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute monad result collection function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="okFunc">Function if predicate <see langword="true"/></param>
        /// <param name="badFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> ResultCollectionBindWhere<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> okFunc,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> badFunc) 
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? okFunc.Invoke(@this.GetValue())
                 : badFunc.Invoke(@this.GetValue())
             : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute monad result collection function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if result collection hasn't errors</param>
        /// <param name="badFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> ResultCollectionBindOkBad<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                        Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> okFunc,
                                                                                        Func<IReadOnlyCollection<IRError>, IRList<TValueOut>> badFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? okFunc.Invoke(@this.GetValue())
             : badFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Execute monad result collection function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> ResultCollectionBindOk<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                    Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> okFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? okFunc.Invoke(@this.GetValue())
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute monad result collection function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="badFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValue> ResultCollectionBindBad<TValue>(this IRList<TValue> @this,
                                                                     Func<IReadOnlyCollection<IRError>, IRList<TValue>> badFunc)
            where TValue : notnull =>
            @this.Success
                ? @this
                : badFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Adding errors to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="okFunc">Error function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValue> ResultCollectionBindErrorsOk<TValue>(this IRList<TValue> @this,
                                                                          Func<IReadOnlyCollection<TValue>, IROption> okFunc) 
            where TValue : notnull =>
            @this.
            ResultCollectionBindOk(collection => okFunc.Invoke(collection).ToRList(collection));
    }
}