using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Lists;
using ResultFunctional.Models.Values;

namespace ResultFunctional.Models.Factories
{
    /// <summary>
    /// Result collection factory
    /// </summary>
    public static class RListFactory
    {
        /// <summary>
        /// Create result collection by values
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="values">Values</param>
        /// <returns>Result collection in task</returns>
        public static IRList<TValue> Some<TValue>(IReadOnlyCollection<TValue> values)
            where TValue : notnull =>
            RList<TValue>.Some(values);

        /// <summary>
        /// Create task result collection by values
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="values">Values</param>
        /// <returns>Result collection in task</returns>
        public static Task<IRList<TValue>> SomeTask<TValue>(IReadOnlyCollection<TValue> values)
            where TValue : notnull =>
            Task.FromResult(RList<TValue>.Some(values));

        /// <summary>
        /// Create task result collection by error
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="error">Error</param>
        /// <returns>Result collection in task</returns>
        public static IRList<TValue> None<TValue>(IRError error)
            where TValue : notnull =>
             RList<TValue>.None(error);

        /// <summary>
        /// Create task result collection by error
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="error">Error</param>
        /// <returns>Result collection in task</returns>
        public static Task<IRList<TValue>> NoneTask<TValue>(IRError error)
            where TValue : notnull =>
            Task.FromResult(RList<TValue>.None(error));

        /// <summary>
        /// Create task result collection by errors
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="errors">Errors</param>
        /// <returns>Result collection in task</returns>
        public static IRList<TValue> None<TValue>(IReadOnlyCollection<IRError> errors)
            where TValue : notnull =>
            RList<TValue>.None(errors);

        /// <summary>
        /// Create task result collection by errors
        /// </summary>
        /// <typeparam name="TValue">Value parameter</typeparam>
        /// <param name="errors">Errors</param>
        /// <returns>Result collection in task</returns>
        public static Task<IRList<TValue>> NoneTask<TValue>(IReadOnlyCollection<IRError> errors)
            where TValue : notnull =>
            Task.FromResult(RList<TValue>.None(errors));
    }
}