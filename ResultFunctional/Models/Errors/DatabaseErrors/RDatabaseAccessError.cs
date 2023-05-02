using System;
using ResultFunctional.Models.Enums;

namespace ResultFunctional.Models.Errors.DatabaseErrors
{
    /// <summary>
    /// Table access database error
    /// </summary>
    public class RDatabaseAccessError : RDatabaseError<IRDatabaseAccessError>, IRDatabaseAccessError
    {
        /// <summary>
        /// Initialize access database error
        /// </summary>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        public RDatabaseAccessError(string tableName, string description)
          : this(DatabaseErrorType.TableAccess, tableName, description)
        { }

        /// <summary>
        /// Initialize access database error
        /// </summary>
        /// <param name="databaseErrorType">Database error type</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        protected RDatabaseAccessError(DatabaseErrorType databaseErrorType, string tableName, string description)
            : this(databaseErrorType, tableName, description, null)
        { }

        /// <summary>
        /// Initialize access database error
        /// </summary>
        /// <param name="databaseErrorType">Database error type</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RDatabaseAccessError(DatabaseErrorType databaseErrorType, string tableName, string description, Exception? exception)
            : base(databaseErrorType, tableName, description, exception)
        { }

        /// <summary>
        /// Initialize access database error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Access database error</returns>
        protected override IRDatabaseAccessError InitializeType(string description, Exception? exception) =>
            new RDatabaseAccessError(DatabaseErrorType.TableAccess, TableName, description, exception);
    }
}