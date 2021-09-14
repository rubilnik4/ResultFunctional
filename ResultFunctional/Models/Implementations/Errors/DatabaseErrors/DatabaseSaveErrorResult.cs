using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка сохранения базы данных
    /// </summary>
    public class DatabaseSaveErrorResult : ErrorBaseResult<DatabaseErrorType, DatabaseSaveErrorResult>
    {
        public DatabaseSaveErrorResult(string description)
            : this(description, null)
        { }

        protected DatabaseSaveErrorResult(string description, Exception? exception)
            : base(DatabaseErrorType.Save, description, exception)
        { }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new DatabaseSaveErrorResult(description, exception);
    }
}