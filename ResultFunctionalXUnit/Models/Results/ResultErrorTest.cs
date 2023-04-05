using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Errors.DatabaseErrors;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Implementations.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.Models.Results
{
    /// <summary>
    /// Базовый вариант ответа. Тест
    /// </summary>
    public class ResultErrorTest
    {
        /// <summary>
        /// Базовая инициализация. Отсутствие ошибок
        /// </summary>
        [Fact]
        public void Initialize_Base_OkStatus()
        {
            var resultError = new ResultError();

            Assert.True(resultError.OkStatus);
            Assert.False(resultError.HasErrors);
            Assert.Empty(resultError.Errors);
        }

        /// <summary>
        /// Инициализация одной ошибкой
        /// </summary>
        [Fact]
        public void Initialize_Error_HasOne()
        {
            var resultError = new ResultError(CreateErrorTest());

            Assert.False(resultError.OkStatus);
            Assert.True(resultError.HasErrors);
            Assert.Single(resultError.Errors);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void AppendError()
        {
            var resultErrorInitial = new ResultError();
            var errorToConcat = CreateErrorTest();

            var resultErrorConcat = resultErrorInitial.AppendError(errorToConcat);

            Assert.True(resultErrorConcat.HasErrors);
            Assert.Equal(1, resultErrorConcat.Errors.Count);
            Assert.True(errorToConcat.Equals(resultErrorConcat.Errors.Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalOne()
        {
            var resultErrorInitial = new ResultError();
            var errorToConcat = CreateErrorTest();

            var resultErrorConcat = resultErrorInitial.ConcatErrors(errorToConcat);

            Assert.True(resultErrorConcat.HasErrors);
            Assert.Equal(1, resultErrorConcat.Errors.Count);
            Assert.True(errorToConcat.Equals(resultErrorConcat.Errors.Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var errorToConcat = CreateErrorTest();
            var resultErrorInitial = new ResultError(errorToConcat);

            var resultErrorConcat = resultErrorInitial.ConcatErrors(errorToConcat);

            Assert.True(resultErrorConcat.HasErrors);
            Assert.Equal(2, resultErrorConcat.Errors.Count);
            Assert.True(errorToConcat.Equals(resultErrorConcat.Errors.Last()));
        }

        /// <summary>
        /// Наличие специфической ошибки
        /// </summary>
        [Fact]
        public void HasError()
        {
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");
            var result = databaseTableError.ToRUnit();

            Assert.True(result.HasError<IRDatabaseAccessError>());
            Assert.True(result.HasError<RDatabaseAccessError>());
        }

        /// <summary>
        /// Наличие специфической ошибки
        /// </summary>
        [Fact]
        public void IsError()
        {
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");
            var result = databaseTableError.ToRUnit();

            Assert.False(result.IsError<IRDatabaseAccessError>());
            Assert.True(result.IsError<RDatabaseAccessError>());
        }

        /// <summary>
        /// Получить тиы ошибок
        /// </summary>
        [Fact]
        public void GetErrorTypes()
        {
            var valueNotFoundError = RErrorFactory.ValueNotFound<string>("value");
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");
            var errors = new List<IRError> { valueNotFoundError, databaseTableError };
            var result = new ResultError(errors);

            var errorTypes = result.GetErrorByTypes();

            Assert.True(errorTypes.SequenceEqual(errors.Select(error => error.GetType())));
        }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        [Fact]
        public void HasErrorType()
        {
            var valueNotFoundError = RErrorFactory.ValueNotFound<string>("value");
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");
            var errors = new List<IRError> { valueNotFoundError, databaseTableError };
            var result = new ResultError(errors);

            Assert.True(result.HasErrorType<DatabaseErrorType>());
        }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        [Fact]
        public void HasErrorTypeByType()
        {
            var valueNotFoundError = RErrorFactory.ValueNotFound<string>("value");
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");
            var errors = new List<IRError> { valueNotFoundError, databaseTableError };
            var result = new ResultError(errors);

            Assert.True(result.HasErrorType(CommonErrorType.ValueNotFound));
            Assert.False(result.HasErrorType(CommonErrorType.ValueNotValid));
        }

        /// <summary>
        /// Получить тип ошибки
        /// </summary>
        [Fact]
        public void GetErrorType()
        {
            var valueNotFoundError = RErrorFactory.ValueNotFound<string>("value");
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");
            var errors = new List<IRError> { valueNotFoundError, databaseTableError };
            var result = new ResultError(errors);

            var errorType = result.GetErrorByType<DatabaseErrorType>();
            Assert.NotNull(errorType);
            Assert.Equal(DatabaseErrorType.TableAccess, errorType?.ErrorType);
        }

        /// <summary>
        /// Получить типы ошибок
        /// </summary>
        [Fact]
        public void GetErrorTypesT()
        {
            var valueNotFoundError = RErrorFactory.ValueNotFound<string>("value");
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");
            var errors = new List<IRError> { valueNotFoundError, databaseTableError };
            var result = new ResultError(errors);

            var errorTypes = result.GetErrorsByTypes<DatabaseErrorType>();
            Assert.Equal(1, errorTypes.Count);
            Assert.Equal(DatabaseErrorType.TableAccess, errorTypes.First().ErrorType);
        }
    }
}