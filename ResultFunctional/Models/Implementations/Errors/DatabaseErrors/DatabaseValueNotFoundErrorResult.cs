using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Database not found value error
    /// </summary>
    /// <typeparam name="TValue">Database value</typeparam>
    public class DatabaseValueNotFoundErrorResult<TValue> : DatabaseValueErrorResult<TValue, IDatabaseValueNotFoundErrorResult>, 
                                                            IDatabaseValueNotFoundErrorResult
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        where TValue : notnull
#endif
    {
        /// <summary>
        /// Initialize database not found value error
        /// </summary>
        /// <param name="value">Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        public DatabaseValueNotFoundErrorResult(TValue value, string tableName, string description)
            : this(value, tableName, description, null)
        { }

        /// <summary>
        /// Initialize database not found value error
        /// </summary>
        /// <param name="value">Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected DatabaseValueNotFoundErrorResult(TValue value, string tableName, string description, Exception? exception)
           : base(DatabaseErrorType.ValueNotFound, value, tableName, description, exception)
        { }

        /// <summary>
        /// Initialize database not found value error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Duplicate database error</returns>
        protected override IDatabaseValueNotFoundErrorResult InitializeType(string description, Exception? exception) =>
            new DatabaseValueNotFoundErrorResult<TValue>(Value, TableName, description, exception);
    }
}