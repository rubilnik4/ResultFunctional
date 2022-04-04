using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.ConversionErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Database connection error
    /// </summary>
    public class DatabaseConnectionErrorResult : ErrorBaseResult<DatabaseErrorType, DatabaseConnectionErrorResult>
    {
        /// <summary>
        /// Initialize database connection error
        /// </summary>
        /// <param name="parameter">Database parameter name</param>
        /// <param name="description">Description</param>
        public DatabaseConnectionErrorResult(string parameter, string description)
          : this(parameter, description, null)
        { }

        /// <summary>
        /// Initialize database connection error
        /// </summary>
        /// <param name="parameter">Database parameter</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        public DatabaseConnectionErrorResult(string parameter, string description, Exception? exception)
            : base(DatabaseErrorType.Connection, description, exception)
        {
            Parameter = parameter;
        }

        /// <summary>
        /// Database parameter
        /// </summary>
        public string Parameter { get; }

        /// <summary>
        /// Initialize database connection error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Connection database error</returns>
        protected override DatabaseConnectionErrorResult InitializeType(string description, Exception? exception) =>
            new DatabaseConnectionErrorResult(Parameter, description, exception);
    }
}