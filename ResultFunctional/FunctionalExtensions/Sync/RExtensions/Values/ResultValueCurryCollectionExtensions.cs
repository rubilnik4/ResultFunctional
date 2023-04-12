using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values
{
    /// <summary>
    /// Extension methods for result value higher order collection functions
    /// </summary>
    public static class ResultValueCurryCollectionExtensions
    {
        /// <summary>
        /// Get no arguments result value higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value one argument high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static IRValue<Func<TOut>> ResultValueCurryCollectionOk<TIn1, TOut>(this IRValue<Func<IEnumerable<TIn1>, TOut>> @this,
                                                                                   IRList<TIn1> arg1)
            where TIn1 : notnull
            where TOut : notnull =>
            @this.
            ResultValueOk(func => (Func<IReadOnlyCollection<TIn1>, TOut>)func).
            ResultValueCurryCollectionOk(arg1);

        /// <summary>
        /// Get no arguments result value higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value one argument high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static IRValue<Func<TOut>> ResultValueCurryCollectionOk<TIn1, TOut>(this IRValue<Func<IReadOnlyCollection<TIn1>, TOut>> @this,
                                                                                        IRList<TIn1> arg1) 
            where TIn1 : notnull
            where TOut : notnull =>
            @this.Success && arg1.Success
                ? @this.GetValue().Curry(arg1.GetValue()).ToRValue()
                : @this.GetErrorsOrEmpty().Concat(arg1.GetErrorsOrEmpty()).ToRValue<Func<TOut>>();

        /// <summary>
        /// Get one argument result value higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value two arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static IRValue<Func<TIn2, TOut>> ResultValueCurryCollectionOk<TIn1, TIn2, TOut>(this IRValue<Func<IEnumerable<TIn1>, TIn2, TOut>> @this,
                                                                                                    IRList<TIn1> arg1) 
            where TIn1 : notnull
            where TIn2 : notnull
            where TOut : notnull =>
            @this.
            ResultValueOk(func => (Func<IReadOnlyCollection<TIn1>, TIn2, TOut>)func).
            ResultValueCurryCollectionOk(arg1);


        /// <summary>
        /// Get one argument result value higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value two arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static IRValue<Func<TIn2, TOut>> ResultValueCurryCollectionOk<TIn1, TIn2, TOut>(this IRValue<Func<IReadOnlyCollection<TIn1>, TIn2, TOut>> @this,
                                                                                          IRList<TIn1> arg1)
            where TIn1 : notnull
            where TIn2 : notnull
            where TOut : notnull =>
            @this.Success && arg1.Success
                ? @this.GetValue().Curry(arg1.GetValue()).ToRValue()
                : @this.GetErrorsOrEmpty().Concat(arg1.GetErrorsOrEmpty()).ToRValue<Func<TIn2, TOut>>();

        /// <summary>
        /// Get two arguments result value higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value three arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static IRValue<Func<TIn2, TIn3, TOut>> ResultValueCurryCollectionOk<TIn1, TIn2, TIn3, TOut>(this IRValue<Func<IEnumerable<TIn1>, TIn2, TIn3, TOut>> @this,
                                                                                                    IRList<TIn1> arg1)
            where TIn1 : notnull
            where TIn2 : notnull
            where TIn3 : notnull
            where TOut : notnull =>
            @this.
            ResultValueOk(func => (Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TOut>)func).
            ResultValueCurryCollectionOk(arg1);

        /// <summary>
        /// Get two arguments result value higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value three arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static IRValue<Func<TIn2, TIn3, TOut>> ResultValueCurryCollectionOk<TIn1, TIn2, TIn3, TOut>(this IRValue<Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TOut>> @this,
                                                                                                    IRList<TIn1> arg1)
            where TIn1 : notnull
            where TIn2 : notnull
            where TIn3 : notnull
            where TOut : notnull =>
            @this.Success && arg1.Success
                ? @this.GetValue().Curry(arg1.GetValue()).ToRValue()
                : @this.GetErrorsOrEmpty().Concat(arg1.GetErrorsOrEmpty()).ToRValue<Func<TIn2, TIn3, TOut>>();

        /// <summary>
        /// Get three arguments result value higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value four arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static IRValue<Func<TIn2, TIn3, TIn4, TOut>> ResultValueCurryCollectionOk<TIn1, TIn2, TIn3, TIn4, TOut>(this IRValue<Func<IEnumerable<TIn1>, TIn2, TIn3, TIn4, TOut>> @this,
                                                                                                                            IRList<TIn1> arg1)
            where TIn1 : notnull
            where TIn2 : notnull
            where TIn3 : notnull
            where TIn4 : notnull
            where TOut : notnull =>
            @this.
            ResultValueOk(func => (Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TIn4, TOut>)func).
            ResultValueCurryCollectionOk(arg1);

        /// <summary>
        /// Get three arguments result value higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value four arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static IRValue<Func<TIn2, TIn3, TIn4, TOut>> ResultValueCurryCollectionOk<TIn1, TIn2, TIn3, TIn4, TOut>(this IRValue<Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TIn4, TOut>> @this,
                                                                                                                  IRList<TIn1> arg1)
            where TIn1 : notnull
            where TIn2 : notnull
            where TIn3 : notnull
            where TIn4 : notnull
            where TOut : notnull =>
             @this.Success && arg1.Success
                 ? @this.GetValue().Curry(arg1.GetValue()).ToRValue()
                 : @this.GetErrorsOrEmpty().Concat(arg1.GetErrorsOrEmpty()).ToRValue<Func<TIn2, TIn3, TIn4, TOut>>();

        /// <summary>
        /// Get four arguments result value higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TIn5">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value five arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static IRValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>> ResultValueCurryCollectionOk<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this IRValue<Func<IEnumerable<TIn1>, TIn2, TIn3, TIn4, TIn5, TOut>> @this,
                                                                                                                                        IRList<TIn1> arg1)
            where TIn1 : notnull
            where TIn2 : notnull
            where TIn3 : notnull
            where TIn4 : notnull
            where TIn5 : notnull
            where TOut : notnull =>
            @this.
            ResultValueOk(func => (Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TIn4, TIn5, TOut>)func).
            ResultValueCurryCollectionOk(arg1);

        /// <summary>
        /// Get four arguments result value higher order collection function
        /// </summary>
        /// <typeparam name="TIn1">Argument type</typeparam>
        /// <typeparam name="TIn2">Argument type</typeparam>
        /// <typeparam name="TIn3">Argument type</typeparam>
        /// <typeparam name="TIn4">Argument type</typeparam>
        /// <typeparam name="TIn5">Argument type</typeparam>
        /// <typeparam name="TOut">Function type</typeparam>
        /// <param name="this">Result value five arguments high order function</param>
        /// <param name="arg1">Result collection argument</param>
        /// <returns>Result value higher order function</returns>
        public static IRValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>> ResultValueCurryCollectionOk<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this IRValue<Func<IReadOnlyCollection<TIn1>, TIn2, TIn3, TIn4, TIn5, TOut>> @this,
                                                                                                                             IRList<TIn1> arg1)
            where TIn1 : notnull
            where TIn2 : notnull
            where TIn3 : notnull
            where TIn4 : notnull
            where TIn5 : notnull
            where TOut : notnull =>
            @this.Success && arg1.Success
                 ? @this.GetValue().Curry(arg1.GetValue()).ToRValue()
                 : @this.GetErrorsOrEmpty().Concat(arg1.GetErrorsOrEmpty()).ToRValue<Func<TIn2, TIn3, TIn4, TIn5, TOut>>();
    }
}