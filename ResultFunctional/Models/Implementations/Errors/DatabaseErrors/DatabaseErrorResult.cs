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
    /// Ошибка базы данных
    /// </summary>
    public abstract class DatabaseErrorResult<TErrorResult> : ErrorBaseResult<DatabaseErrorType, TErrorResult>
          where TErrorResult : IDatabaseErrorResult
    {
        protected DatabaseErrorResult(DatabaseErrorType databaseErrorType, string tableName, string description)
            : this(databaseErrorType, tableName, description, null)
        { }

        protected DatabaseErrorResult(DatabaseErrorType databaseErrorType, string tableName, string description, Exception? exception)
            : base(databaseErrorType, description, exception)
        {
            TableName = tableName;
        }

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public string TableName { get; }
    }
}