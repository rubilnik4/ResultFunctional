using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Database error
    /// </summary>
    /// <typeparam name="TErrorResult">Database error type</typeparam>
    public abstract class DatabaseErrorResult<TErrorResult> : ErrorBaseResult<DatabaseErrorType, TErrorResult>
          where TErrorResult : IDatabaseErrorResult
    {
        /// <summary>
        /// Initialize database error
        /// </summary>
        /// <param name="databaseErrorType">Database error type</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        protected DatabaseErrorResult(DatabaseErrorType databaseErrorType, string tableName, string description)
            : this(databaseErrorType, tableName, description, null)
        { }

        /// <summary>
        /// Initialize database error
        /// </summary>
        /// <param name="databaseErrorType">Database error type</param>
        /// <param name="tableName">Table name</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected DatabaseErrorResult(DatabaseErrorType databaseErrorType, string tableName, string description, Exception? exception)
            : base(databaseErrorType, description, exception)
        {
            TableName = tableName;
        }

        /// <summary>
        /// Database table name
        /// </summary>
        public string TableName { get; }
    }
}