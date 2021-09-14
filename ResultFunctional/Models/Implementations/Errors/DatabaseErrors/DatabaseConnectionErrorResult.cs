using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.ConversionErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace ResultFunctional.Models.Implementations.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка подключения к базе данных
    /// </summary>
    public class DatabaseConnectionErrorResult : ErrorBaseResult<DatabaseErrorType, DatabaseConnectionErrorResult>
    {
        public DatabaseConnectionErrorResult(string parameter, string description)
          : this(parameter, description, null)
        { }

        public DatabaseConnectionErrorResult(string parameter, string description, Exception? exception)
            : base(DatabaseErrorType.Connection, description, exception)
        {
            Parameter = parameter;
        }

        /// <summary>
        /// Имя параметра
        /// </summary>
        public string Parameter { get; }

        /// <summary>
        /// Инициализация ошибки
        /// </summary>
        protected override IErrorResult Initialize(string description, Exception? exception) =>
            new DatabaseConnectionErrorResult(Parameter, description, exception);
    }
}