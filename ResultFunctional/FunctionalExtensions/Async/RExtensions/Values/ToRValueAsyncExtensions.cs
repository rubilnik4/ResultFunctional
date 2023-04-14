using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Result value async extension methods
    /// </summary>
    public static class ToRValueAsyncExtensions
    {
        /// <summary>
        /// Converting value to result value with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueEnsureAsync<TValue>(this TValue @this, Task<IRError> error)
            where TValue : notnull =>
            @this.ToRValueEnsure(await error);

        /// <summary>
        /// Converting value to result value async with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueNullEnsureAsync<TValue>(this TValue? @this, Task<IRError> error)
            where TValue : class =>
            @this.ToRValueNullEnsure(await error);

        /// <summary>
        /// Converting value to result value async with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueNullEnsureAsync<TValue>(this TValue? @this,
                                                                                           Task<IRError> error)
            where TValue : struct =>
            @this.ToRValueNullEnsure(await error);

        /// <summary>
        /// Async converting result unit to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="value">Value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueAsync<TValue>(this IRUnit @this, Task<TValue> value)
            where TValue : notnull =>
            @this.Success
                ? await value.ToRValueTask()
                : @this.GetErrors().ToRValue<TValue>();

        /// <summary>
        /// Async converting result option to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="value">Value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueAsync<TValue>(this IROption @this, Task<TValue> value)
            where TValue : notnull =>
            @this.Success
                ? await value.ToRValueTask()
                : @this.GetErrors().ToRValue<TValue>();

        /// <summary>
        /// Async merge result unit with result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="resultValue">Result value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueBindAsync<TValue>(this IRUnit @this, Task<IRValue<TValue>> resultValue)
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
        public static async Task<IRValue<TValue>> ToRValueBindAsync<TValue>(this IROption @this, Task<IRValue<TValue>> resultValue)
            where TValue : notnull =>
            @this.Success
                ? await resultValue
                : @this.GetErrors().ToRValue<TValue>();
    }
}