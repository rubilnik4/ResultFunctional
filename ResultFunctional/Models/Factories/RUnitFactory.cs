using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Units;

namespace ResultFunctional.Models.Factories
{
    /// <summary>
    /// Result unit factory
    /// </summary>
    public static class RUnitFactory
    {
        /// <summary>
        /// Create result unit
        /// </summary>
        /// <returns>Result unit</returns>
        public static IRUnit Some() =>
            RUnit.Some();

        /// <summary>
        /// Create result unit
        /// </summary>
        /// <returns>Result unit in task</returns>
        public static Task<IRUnit> SomeTask() =>
            Task.FromResult(RUnit.Some());

        /// <summary>
        /// Create result unit by error
        /// </summary>
        /// <param name="error">Error</param>
        /// <returns>Result unit</returns>
        public static IRUnit None(IRError error) =>
            RUnit.None(error);

        /// <summary>
        /// Create task result unit by error
        /// </summary>
        /// <param name="error">Error</param>
        /// <returns>Result unit in task</returns>
        public static Task<IRUnit> NoneTask(IRError error) =>
            Task.FromResult(RUnit.None(error));

        /// <summary>
        /// Create result unit by errors
        /// </summary>
        /// <param name="errors">Errors</param>
        /// <returns>Result unit in task</returns>
        public static IRUnit None(IReadOnlyCollection<IRError> errors) =>
            RUnit.None(errors);
        
        /// <summary>
        /// Create task result unit by errors
        /// </summary>
        /// <param name="errors">Errors</param>
        /// <returns>Result unit in task</returns>
        public static Task<IRUnit> NoneTask(IReadOnlyCollection<IRError> errors) =>
            Task.FromResult(RUnit.None(errors));

        /// <summary>
        /// Create task result unit by result
        /// </summary>
        /// <param name="error">Result error</param>
        /// <returns>Result unit in task</returns>
        public static Task<IRUnit> NoneTask(IRUnit error) =>
            Task.FromResult(error);
    }
}