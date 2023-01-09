using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Factories
{
    /// <summary>
    /// Result error factory
    /// </summary>
    public static class ResultErrorFactory
    {
        /// <summary>
        /// Create result error
        /// </summary>
        /// <returns>Result error in task</returns>
        public static IResultError CreateResultError() =>
            new ResultError();

        /// <summary>
        /// Create task result error
        /// </summary>
        /// <returns>Result error in task</returns>
        public static Task<IResultError> CreateTaskResultError() =>
            Task.FromResult((IResultError)new ResultError());

        /// <summary>
        /// Create result error by error
        /// </summary>
        /// <param name="error">Error</param>
        /// <returns>Result error in task</returns>
        public static IResultError CreateResultError(IRError error) =>
            new ResultError(error);

        /// <summary>
        /// Create task result error by error
        /// </summary>
        /// <param name="error">Error</param>
        /// <returns>Result error in task</returns>
        public static Task<IResultError> CreateTaskResultError(IRError error) =>
            Task.FromResult((IResultError)new ResultError(error));

        /// <summary>
        /// Create result error by errors
        /// </summary>
        /// <param name="errors">Errors</param>
        /// <returns>Result error in task</returns>
        public static IResultError CreateResultError(IEnumerable<IRError> errors) =>
            new ResultError(errors);


        /// <summary>
        /// Create task result error by errors
        /// </summary>
        /// <param name="errors">Errors</param>
        /// <returns>Result error in task</returns>
        public static Task<IResultError> CreateTaskResultError(IEnumerable<IRError> errors) =>
            Task.FromResult((IResultError)new ResultError(errors));

        /// <summary>
        /// Create task result error by result
        /// </summary>
        /// <param name="error">Result error</param>
        /// <returns>Result error in task</returns>
        public static Task<IResultError> CreateTaskResultError(IResultError error) =>
            Task.FromResult(error);
    }
}