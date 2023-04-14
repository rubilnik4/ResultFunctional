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
    public static class ToRValueAwaitExtensions
    {
        /// <summary>
        /// Converting task value to result value with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueEnsureAwait<TValue>(this Task<TValue> @this, Task<IRError> error)
            where TValue : notnull =>
            await @this.
            MapAwait(thisAwaited => thisAwaited.ToRValueEnsureAsync(error));

        /// <summary>
        /// Converting task value to result value async with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueNullEnsureAwait<TValue>(this Task<TValue?> @this,
                                                                                               Task<IRError> error)
            where TValue : class =>
            await @this.
            MapAwait(thisAwaited => thisAwaited.ToRValueNullEnsureAsync(error));

        /// <summary>
        /// Converting task value to result value async with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueNullEnsureAwait<TValue>(this Task<TValue?> @this,
                                                                                               Task<IRError> error)
            where TValue : struct =>
            await @this.
            MapAwait(thisAwaited => thisAwaited.ToRValueNullEnsureAsync(error));

        /// <summary>
        /// Converting task result unit to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="value">Value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueAwait<TValue>(this Task<IRUnit> @this, Task<TValue> value)
            where TValue : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.ToRValueAsync(value));



        /// <summary>
        /// Converting task result option to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="value">Value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueAwait<TValue>(this Task<IROption> @this, Task<TValue> value)
            where TValue : notnull =>
            await @this.
            MapAwait(awaitedThis => awaitedThis.ToRValueAsync(value));

        /// <summary>
        /// Async merge task result unit with result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="resultValue">Result value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueBindAwait<TValue>(this Task<IRUnit> @this, Task<IRValue<TValue>> resultValue)
            where TValue : notnull =>
            await @this.
            MapAwait(result => result.ToRValueBindAsync(resultValue));

        /// <summary>
        /// Async merge task result option with result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="resultValue">Result value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IRValue<TValue>> ToRValueBindAwait<TValue>(this Task<IROption> @this, Task<IRValue<TValue>> resultValue)
            where TValue : notnull =>
            await @this.
            MapAwait(result => result.ToRValueBindAsync(resultValue));
    }
}