using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Errors.DatabaseErrors
{
    /// <summary>
    /// Database connection error
    /// </summary>
    public class RDatabaseConnectionError : RBaseError<DatabaseErrorType, RDatabaseConnectionError>
    {
        /// <summary>
        /// Initialize database connection error
        /// </summary>
        /// <param name="parameter">Database parameter name</param>
        /// <param name="description">Description</param>
        public RDatabaseConnectionError(string parameter, string description)
          : this(parameter, description, null)
        { }

        /// <summary>
        /// Initialize database connection error
        /// </summary>
        /// <param name="parameter">Database parameter</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        public RDatabaseConnectionError(string parameter, string description, Exception? exception)
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
        protected override RDatabaseConnectionError InitializeType(string description, Exception? exception) =>
            new(Parameter, description, exception);
    }
}