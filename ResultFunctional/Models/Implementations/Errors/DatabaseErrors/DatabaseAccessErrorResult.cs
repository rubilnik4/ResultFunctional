using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка таблицы базы данных
    /// </summary>
    public class DatabaseAccessErrorResult : DatabaseErrorResult<IDatabaseAccessErrorResult>, IDatabaseAccessErrorResult
    {
        public DatabaseAccessErrorResult(string tableName, string description)
          : this(DatabaseErrorType.TableAccess, tableName, description)
        { }

        protected DatabaseAccessErrorResult(DatabaseErrorType databaseErrorType, string tableName, string description)
            : this(databaseErrorType, tableName, description, null)
        { }

        protected DatabaseAccessErrorResult(DatabaseErrorType databaseErrorType, string tableName, string description,
                                           Exception? exception)
            : base(databaseErrorType, tableName, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new DatabaseAccessErrorResult(DatabaseErrorType.TableAccess, TableName, description, exception);
    }
}