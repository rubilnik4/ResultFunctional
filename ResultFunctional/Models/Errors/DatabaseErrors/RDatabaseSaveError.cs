using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.DatabaseErrors
{
    /// <summary>
    /// Database save transaction error
    /// </summary>
    public class RDatabaseSaveError : RBaseError<DatabaseErrorType, RDatabaseSaveError>
    {
        /// <summary>
        /// Initialize database save transaction error
        /// </summary>
        /// <param name="description">Description</param>
        public RDatabaseSaveError(string description)
            : this(description, null)
        { }

        /// <summary>
        /// Initialize database save transaction error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        protected RDatabaseSaveError(string description, Exception? exception)
            : base(DatabaseErrorType.Save, description, exception)
        { }

        /// <summary>
        /// Initialize database save transaction error
        /// </summary>
        /// <param name="description">Description</param>
        /// <param name="exception">Exception</param>
        /// <returns>Transaction database error</returns>
        protected override RDatabaseSaveError InitializeType(string description, Exception? exception) =>
            new(description, exception);
    }
}