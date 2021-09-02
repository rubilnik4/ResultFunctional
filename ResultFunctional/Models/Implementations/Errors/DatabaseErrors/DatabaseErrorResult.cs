using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка базы данных
    /// </summary>
    public class DatabaseErrorResult : ErrorBaseResult<DatabaseErrorType>
    {
        public DatabaseErrorResult(DatabaseErrorType databaseErrorType, string description)
            : this(databaseErrorType, description, null)
        { }

        public DatabaseErrorResult(DatabaseErrorType databaseErrorType, string description, Exception? exception)
            : base(databaseErrorType, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new DatabaseErrorResult(ErrorType, description, exception);
    }
}