using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues
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
        public static async Task<IResultValue<TValue>> ToResultValueNullValueCheckBindAsync<TValue>(this Task<TValue> @this,
                                                                                                    Task<IRError> error) =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueNullValueCheckAsync(error));

        /// <summary>
        /// Converting task value to result value async with null checking
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming value</param>
        /// <param name="error">Null error</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckBindAsync<TValue>(this Task<TValue?> @this,
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
        public static async Task<IResultValue<TValue>> ToResultValueNullCheckBindAsync<TValue>(this Task<TValue?> @this,
                                                                                               Task<IRError> error)
            where TValue : struct =>
            await @this.
            MapBindAsync(thisAwaited => thisAwaited.ToResultValueNullCheckAsync(error));

        /// <summary>
        /// Converting task result error to result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="value">Value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ToResultValueBindAsync<TValue>(this Task<IResultError> @this, Task<TValue> value) =>
            await @this.
            MapBindAsync(awaitedThis => awaitedThis.ToResultValueAsync(value));

        /// <summary>
        /// Async merge task result error with result value
        /// </summary>
        /// <typeparam name="TValue">Result type</typeparam>
        /// <param name="this">Incoming result error</param>
        /// <param name="resultValue">Result value</param>
        /// <returns>Outgoing result value</returns>
        public static async Task<IResultValue<TValue>> ToResultBindValueBindAsync<TValue>(this Task<IResultError> @this,
                                                                                          Task<IResultValue<TValue>> resultValue) =>
            await @this.
            MapBindAsync(result => result.ToResultBindValueAsync(resultValue));
    }
}