﻿using System;
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
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> ResultCollectionBindContinue<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                          Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                          Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> someFunc,
                                                                                          Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? someFunc.Invoke(@this.GetValue())
                 : noneFunc.Invoke(@this.GetValue()).ToRList<TValueOut>()
             : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute monad result collection function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> ResultCollectionBindWhere<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> someFunc,
                                                                                                 Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> noneFunc) 
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? someFunc.Invoke(@this.GetValue())
                 : noneFunc.Invoke(@this.GetValue())
             : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute monad result collection function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> ResultCollectionBindOkBad<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                        Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> someFunc,
                                                                                        Func<IReadOnlyCollection<IRError>, IRList<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? someFunc.Invoke(@this.GetValue())
             : noneFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Execute monad result collection function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> ResultCollectionBindOk<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                                    Func<IReadOnlyCollection<TValueIn>, IRList<TValueOut>> someFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? someFunc.Invoke(@this.GetValue())
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute monad result collection function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="noneFunc">Function if incoming result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValue> ResultCollectionBindBad<TValue>(this IRList<TValue> @this,
                                                                     Func<IReadOnlyCollection<IRError>, IRList<TValue>> noneFunc)
            where TValue : notnull =>
            @this.Success
                ? @this
                : noneFunc.Invoke(@this.GetErrors());

        /// <summary>
        /// Adding errors to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Error function if incoming result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValue> ResultCollectionBindErrorsOk<TValue>(this IRList<TValue> @this,
                                                                          Func<IReadOnlyCollection<TValue>, IROption> someFunc) 
            where TValue : notnull =>
            @this.
            ResultCollectionBindOk(collection => someFunc.Invoke(collection).ToRList(collection));
    }
}