using System;
using System.Collections.Generic;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists
{
    /// <summary>
    /// Extension methods for result collection functor function with conditions
    /// </summary>
    public static class RListOptionExtensions
    {
        /// <summary>
        /// Execute result collection function base on predicate condition
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function returning errors if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> RListOption<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                         Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                         Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> someFunc,
                                                                         Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<IRError>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? someFunc.Invoke(@this.GetValue()).ToRList()
                 : noneFunc.Invoke(@this.GetValue()).ToRList<TValueOut>()
             : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute result collection function base on predicate condition returning collection in any case
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="someFunc">Function if predicate <see langword="true"/></param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> RListWhere<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                        Func<IReadOnlyCollection<TValueIn>, bool> predicate,
                                                                        Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> someFunc,
                                                                        Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
         @this.Success
             ? predicate(@this.GetValue())
                 ? someFunc.Invoke(@this.GetValue()).ToRList()
                 : noneFunc.Invoke(@this.GetValue()).ToRList()
             : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute result collection function depending on result collection errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> RListMatch<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                        Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> someFunc,
                                                                        Func<IReadOnlyCollection<IRError>, IReadOnlyCollection<TValueOut>> noneFunc)
            where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? someFunc.Invoke(@this.GetValue()).ToRList()
                : noneFunc.Invoke(@this.GetErrors()).ToRList();

        /// <summary>
        /// Execute result collection function if incoming result collection hasn't errors
        /// </summary>
        /// <typeparam name="TValueIn">Incoming type</typeparam>
        /// <typeparam name="TValueOut">Outgoing type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="someFunc">Function if result collection hasn't errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValueOut> RListSome<TValueIn, TValueOut>(this IRList<TValueIn> @this,
                                                                       Func<IReadOnlyCollection<TValueIn>, IReadOnlyCollection<TValueOut>> someFunc)
             where TValueIn : notnull
            where TValueOut : notnull =>
            @this.Success
                ? someFunc.Invoke(@this.GetValue()).ToRList()
                : @this.GetErrors().ToRList<TValueOut>();

        /// <summary>
        /// Execute result collection function if incoming result collection has errors
        /// </summary>
        /// <typeparam name="TValue">Incoming type</typeparam>
        /// <param name="this">Incoming result collection</param>
        /// <param name="noneFunc">Function if result collection has errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRList<TValue> RListNone<TValue>(this IRList<TValue> @this,
                                                       Func<IReadOnlyCollection<IRError>, IReadOnlyCollection<TValue>> noneFunc)
            where TValue : notnull =>
            @this.Success
                ? @this
                : noneFunc.Invoke(@this.GetErrors()).ToRList();

        /// <summary>
        /// Check errors by predicate to result collection if ones hasn't errors
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Result collection</param>
        /// <param name="predicate">Predicate function</param>
        /// <param name="noneFunc">Function if predicate <see langword="false"/></param>
        /// <returns>Result collection</returns>
        public static IRList<TValue> RListEnsure<TValue>(this IRList<TValue> @this,
                                                         Func<IReadOnlyCollection<TValue>, bool> predicate,
                                                         Func<IReadOnlyCollection<TValue>, IReadOnlyCollection<IRError>> noneFunc)
            where TValue : notnull =>
            @this.
            RListOption(predicate,
                        value => value,
                        noneFunc);
    }
}