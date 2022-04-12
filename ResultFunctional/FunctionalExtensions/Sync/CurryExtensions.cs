using System;

namespace ResultFunctional.FunctionalExtensions.Sync
{
    /// <summary>
    /// Методы расширения для функций высшего порядка
    /// </summary>
    public static class CurryExtensions
    {
        /// <summary>
        /// Преобразование функции высшего порядка для одного аргумента
        /// </summary>
        public static Func<TOut> Curry<TIn1, TOut>(this Func<TIn1, TOut> @this, TIn1 arg1) =>
            () => @this(arg1);

        /// <summary>
        /// Преобразование функции высшего порядка для двух аргументов
        /// </summary>
        public static Func<TIn2, TOut> Curry<TIn1, TIn2, TOut>(this Func<TIn1, TIn2, TOut> @this, TIn1 arg1) =>
            arg2 => @this(arg1, arg2);

        /// <summary>
        /// Преобразование функции высшего порядка для трех аргументов
        /// </summary>
        public static Func<TIn2, TIn3, TOut> Curry<TIn1, TIn2, TIn3, TOut>(this Func<TIn1, TIn2, TIn3, TOut> @this, TIn1 arg1) =>
            (arg2, arg3) => @this(arg1, arg2, arg3);

        /// <summary>
        /// Преобразование функции высшего порядка для четырех аргументов
        /// </summary>
        public static Func<TIn2, TIn3, TIn4, TOut> Curry<TIn1, TIn2, TIn3, TIn4, TOut>(this Func<TIn1, TIn2, TIn3, TIn4, TOut> @this, 
                                                                                       TIn1 arg1) =>
            (arg2, arg3, arg4) => @this(arg1, arg2, arg3, arg4);

        /// <summary>
        /// Преобразование функции высшего порядка для пяти аргументов
        /// </summary>
        public static Func<TIn2, TIn3, TIn4, TIn5, TOut> Curry<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(this Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> @this,
                                                                                                   TIn1 arg1) =>
            (arg2, arg3, arg4, arg5) => @this(arg1, arg2, arg3, arg4, arg5);
    }
}