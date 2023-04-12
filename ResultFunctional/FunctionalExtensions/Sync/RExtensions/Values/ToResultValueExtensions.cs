using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;
using System.Collections.Generic;
using System.Linq;

namespace ResultFunctional.FunctionalExtensions.Sync.RExtension.Values
{
    /// <summary>
    /// Result value extension methods
    /// </summary>
    public static class ToResultValueExtensions
    {
        /// <summary>
        /// Converting value to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ToRValue<TValue>(this TValue @this)
            where TValue : notnull =>
            RValueFactory.Some(@this);

        /// <summary>
        /// Converting errors to result collection
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming errors</param>
        /// <returns>Outgoing result collection</returns>
        public static IRValue<TValue> ToRValue<TValue>(this IEnumerable<IRError> @this)
            where TValue : notnull =>
            RValueFactory.None<TValue>(@this.ToList());

        /// <summary>
        /// Converting value to result value with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ToRValueNullValueCheck<TValue>(this TValue? @this, IRError error) 
            where TValue : notnull =>
            @this != null
                ? @this.ToRValue()
                : error.ToRValue<TValue>();

        /// <summary>
        /// Converting value to result value with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ToRValueNullCheck<TValue>(this TValue? @this, IRError error)
            where TValue : class =>
            @this != null
                ? @this.ToRValue()
                : error.ToRValue<TValue>();

        /// <summary>
        /// Converting value to result value with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ToRValueNullCheck<TValue>(this TValue? @this, IRError error)
            where TValue : struct =>
            @this != null
                ? @this.Value.ToRValue()
                : error.ToRValue<TValue>();

        /// <summary>
        /// Converting result error to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="value">Value</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ToRValue<TValue>(this IRUnit @this, TValue value)
            where TValue : notnull =>
            @this.Success
                ? value.ToRValue()
                : @this.GetErrors().ToRValue<TValue>();

        /// <summary>
        /// Merge result error with result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="resultValue">Result value</param>
        /// <returns>Outgoing result value</returns>
        public static IRValue<TValue> ToRValueBind<TValue>(this IRUnit @this, IRValue<TValue> resultValue)
            where TValue : notnull =>
            @this.Success
                ? resultValue
                : @this.GetErrors().ToRValue<TValue>();
    }
}