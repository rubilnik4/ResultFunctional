using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Values;

namespace ResultFunctional.Models.Factories
{
    /// <summary>
    /// Result value factory
    /// </summary>
    public static class RValueFactory
    {
        /// <summary>
        /// Create result value by value
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="value">Value</param>
        /// <returns>Result value in task</returns>
        public static IRValue<TValue> Some<TValue>(TValue value)
            where TValue : notnull =>
            RValue<TValue>.Some(value);

        /// <summary>
        /// Create task result value by value
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="value">Value</param>
        /// <returns>Result value in task</returns>
        public static Task<IRValue<TValue>> SomeTask<TValue>(TValue value)
            where TValue : notnull =>
            Task.FromResult(RValue<TValue>.Some(value));

        /// <summary>
        /// Create result value by error
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="error">Error</param>
        /// <returns>Result value in task</returns>
        public static IRValue<TValue> None<TValue>(IRError error)
            where TValue : notnull =>
            RValue<TValue>.None(error);

        /// <summary>
        /// Create task result value by error
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="error">Error</param>
        /// <returns>Result value in task</returns>
        public static Task<IRValue<TValue>> NoneTask<TValue>(IRError error)
            where TValue : notnull =>
            Task.FromResult(RValue<TValue>.None(error));

        /// <summary>
        /// Create result value by errors
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="errors">Errors</param>
        /// <returns>Result value in task</returns>
        public static IRValue<TValue> None<TValue>(IReadOnlyCollection<IRError> errors)
            where TValue : notnull =>
            RValue<TValue>.None(errors);

        /// <summary>
        /// Create task result value by errors
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="errors">Errors</param>
        /// <returns>Result value in task</returns>
        public static Task<IRValue<TValue>> NoneTask<TValue>(IReadOnlyCollection<IRError> errors)
            where TValue : notnull =>
            Task.FromResult(RValue<TValue>.None(errors));
    }
}