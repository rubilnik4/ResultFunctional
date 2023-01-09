using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Errors.DatabaseErrors
{
    /// <summary>
    /// Database error
    /// </summary>
    /// <typeparam name="TError">Database error type</typeparam>
    public abstract class RDatabaseError<TError> : RBaseError<DatabaseErrorType, TError>
          where TError : IDatabaseErrorResult
    {
        /// <summary>
        /// Initialize database error
        /// </summary>
        /// <param name="databaseErrorType">Database error type</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        protected RDatabaseError(DatabaseErrorType databaseErrorType, string tableName, string description)
            : this(databaseErrorType, tableName, description, null)
        { }

        /// <summary>
        /// Initialize database error
        /// </summary>
        /// <param name="databaseErrorType">Database error type</param>
        /// <param name="tableName">Table name</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RDatabaseError(DatabaseErrorType databaseErrorType, string tableName, string description, Exception? exception)
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