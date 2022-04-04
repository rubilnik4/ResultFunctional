using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Database not valid value error
    /// </summary>
    /// <typeparam name="TValue">Database value</typeparam>
    public class DatabaseValueNotValidErrorResult<TValue> : DatabaseValueErrorResult<TValue, IDatabaseValueNotValidErrorResult>,
                                                            IDatabaseValueNotValidErrorResult
#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        where TValue : notnull
#endif
    {
        /// <summary>
        /// Initialize database not valid value error
        /// </summary>
        /// <param name="value">Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        public DatabaseValueNotValidErrorResult(TValue value, string tableName, string description)
            : this(value, tableName, description, null)
        { }

        /// <summary>
        /// Initialize database not valid value error
        /// </summary>
        /// <param name="value">Database value</param>
        /// <param name="tableName">Database table name</param>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected DatabaseValueNotValidErrorResult(TValue value, string tableName, string description, Exception? exception)
           : base(DatabaseErrorType.ValueNotValid, value, tableName, description, exception)
        { }

        /// <summary>
        /// Initialize database not valid value error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Duplicate database error</returns>
        protected override IDatabaseValueNotValidErrorResult InitializeType(string description, Exception? exception) =>
            new DatabaseValueNotValidErrorResult<TValue>(Value, TableName, description, exception);
    }
}