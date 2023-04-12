using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Options;
using ResultFunctional.Models.Units;
using ResultFunctional.Models.Values;

namespace ResultFunctional.FunctionalExtensions.Async.RExtensions.Values
{
    /// <summary>
    /// Task result value async extension methods
    /// </summary>
    public static class ToResultValueBindAsyncExtensions
    {
        /// <summary>
        /// Converting task value to result value with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueNullValueCheckBindAsync<TValue>(this Task<TValue> @this,
                                                                                               Task<IRError> error)
            where TValue : notnull =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueNullValueCheckAsync(error));

        /// <summary>
        /// Converting task value to result value async with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueNullCheckBindAsync<TValue>(this Task<TValue?> @this,
                                                                                               Task<IRError> error)
            where TValue : class =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueNullCheckAsync(error));

        /// <summary>
        /// Converting task value to result value async with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueNullCheckBindAsync<TValue>(this Task<TValue?> @this,
                                                                                               Task<IRError> error)
            where TValue : struct =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueNullCheckAsync(error));

        /// <summary>
        /// Converting task result unit to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="value">Value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueBindAsync<TValue>(this Task<IRUnit> @this, Task<TValue> value)
            where TValue : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ToResultValueAsync(value));



        /// <summary>
        /// Converting task result option to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="value">Value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultValueBindAsync<TValue>(this Task<IROption> @this, Task<TValue> value)
            where TValue : notnull =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ToResultValueAsync(value));

        /// <summary>
        /// Async merge task result unit with result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="resultValue">Result value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultBindValueBindAsync<TValue>(this Task<IRUnit> @this,
                                                                                          Task<IRValue<TValue>> resultValue)
            where TValue : notnull =>
            await @this.
            MapBindAsync(result => result.ToResultBindValueAsync(resultValue));

        /// <summary>
        /// Async merge task result option with result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="resultValue">Result value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToResultBindValueBindAsync<TValue>(this Task<IROption> @this,
                                                                                          Task<IRValue<TValue>> resultValue)
            where TValue : notnull =>
            await @this.
            MapBindAsync(result => result.ToResultBindValueAsync(resultValue));
    }
}