using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Table access database error
    /// </summary>
    public class DatabaseAccessErrorResult : DatabaseErrorResult<IDatabaseAccessErrorResult>, IDatabaseAccessErrorResult
    {
        /// <summary>
        /// Initialize access database error
        /// </summary>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        public DatabaseAccessErrorResult(string tableName, string description)
          : this(DatabaseErrorType.TableAccess, tableName, description)
        { }

        /// <summary>
        /// Initialize access database error
        /// </summary>
        /// <param name="databaseErrorType">Database error type</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        protected DatabaseAccessErrorResult(DatabaseErrorType databaseErrorType, string tableName, string description)
            : this(databaseErrorType, tableName, description, null)
        { }

        /// <summary>
        /// Initialize access database error
        /// </summary>
        /// <param name="databaseErrorType">Database error type</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected DatabaseAccessErrorResult(DatabaseErrorType databaseErrorType, string tableName, string description,
                                           Exception? exception)
            : base(databaseErrorType, tableName, description, exception)
        { }

        /// <summary>
        /// Initialize access database error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Access database error</returns>
        protected override IDatabaseAccessErrorResult InitializeType(string description, Exception? exception) =>
            new DatabaseAccessErrorResult(DatabaseErrorType.TableAccess, TableName, description, exception);
    }
}