using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Database save transaction error
    /// </summary>
    public class DatabaseSaveErrorResult : ErrorBaseResult<DatabaseErrorType, DatabaseSaveErrorResult>
    {
        /// <summary>
        /// Initialize database save transaction error
        /// </summary>
        /// <param name="description">Description</param>
        public DatabaseSaveErrorResult(string description)
            : this(description, null)
        { }

        /// <summary>
        /// Initialize database save transaction error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected DatabaseSaveErrorResult(string description, Exception? exception)
            : base(DatabaseErrorType.Save, description, exception)
        { }

        /// <summary>
        /// Initialize database save transaction error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Transaction database error</returns>
        protected override DatabaseSaveErrorResult InitializeType(string description, Exception? exception) =>
            new DatabaseSaveErrorResult(description, exception);
    }
}