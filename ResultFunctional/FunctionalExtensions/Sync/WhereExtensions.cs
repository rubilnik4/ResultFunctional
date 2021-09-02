using System;

namespace ResultFunctional.FunctionalExtensions.Sync
{
    /// <summary>
    /// Методы расширения для проверки условий
    /// </summary>
    public static class WhereTaskExtensions
    {
        /// <summary>
        /// Условие продолжающее действие
        /// </summary>      
        public static TResult WhereContinue<TSource, TResult>(this TSource @this, Func<TSource, bool> predicate,
                                                              Func<TSource, TResult> okFunc, Func<TSource, TResult> badFunc) =>
            predicate(@this) 
                ? okFunc.Invoke(@this)
                : badFunc.Invoke(@this);

        /// <summary>
        /// Обработка позитивного условия
        /// </summary>      
        public static TSource WhereOk<TSource>(this TSource @this, Func<TSource, bool> predicate, Func<TSource, TSource> okFunc) =>
            @this.WhereContinue(predicate, okFunc, _ => @this);

        /// <summary>
        /// Обработка негативного условия
        /// </summary>      
        public static TSource WhereBad<TSource>(this TSource @this, Func<TSource, bool> predicate, Func<TSource, TSource> badFunc) =>
            @this.WhereContinue(predicate, _ => @this, badFunc);
    }
}