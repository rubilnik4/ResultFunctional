using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues
{
    /// <summary>
    /// Result value async extension methods
    /// </summary>
    public static class ToResultValueAsyncExtensions
    {
        /// <summary>
        /// Converting value to result value with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueNullValueCheckAsync<TValue>(this TValue @this,
                                                                                           Task<IRError> error)
            where TValue : notnull =>
            @this.ToRValueNullValueCheck(await error);

        /// <summary>
        /// Converting value to result value async with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueNullCheckAsync<TValue>(this TValue? @this,
                                                                                           Task<IRError> error)
            where TValue : class =>
            @this.ToRValueNullCheck(await error);

        /// <summary>
        /// Converting value to result value async with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueNullCheckAsync<TValue>(this TValue? @this,
                                                                                           Task<IRError> error)
            where TValue : struct =>
            @this.ToRValueNullCheck(await error);

        /// <summary>
        /// Async converting result unit to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="value">Value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueAsync<TValue>(this IRUnit @this, Task<TValue> value)
            where TValue : notnull =>
            @this.Success
                ? await value.ToRValueTaskAsync()
                : @this.GetErrors().ToRValue<TValue>();

        /// <summary>
        /// Async converting result option to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="value">Value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueAsync<TValue>(this IROption @this, Task<TValue> value)
            where TValue : notnull =>
            @this.Success
                ? await value.ToRValueTaskAsync()
                : @this.GetErrors().ToRValue<TValue>();

        /// <summary>
        /// Async merge result unit with result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="resultValue">Result value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultBindValueAsync<TValue>(this IRUnit @this, Task<IRValue<TValue>> resultValue)
            where TValue : notnull =>
            @this.Success
                ? await resultValue
                : @this.GetErrors().ToRValue<TValue>();

        /// <summary>
        /// Async merge result unit with result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="resultValue">Result value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultBindValueAsync<TValue>(this IROption @this, Task<IRValue<TValue>> resultValue)
            where TValue : notnull =>
            @this.Success
                ? await resultValue
                : @this.GetErrors().ToRValue<TValue>();
    }
}