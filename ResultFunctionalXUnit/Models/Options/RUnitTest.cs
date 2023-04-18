using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Errors.DatabaseErrors;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.Models.Options
{
    /// <summary>
    /// Базовый вариант ответа. Тест
    /// </summary>
    public class RUnitTest
    {
        /// <summary>
        /// Базовая инициализация. Отсутствие ошибок
        /// </summary>
        [Fact]
        public void Initialize_Base_OkStatus()
        {
            var resultError = RUnitFactory.Some();

            Assert.True(resultError.Success);
            Assert.False(resultError.Failure);
            Assert.Null(resultError.Errors);
            Assert.Throws<ArgumentNullException>(resultError.GetErrors);
            Assert.Empty(resultError.GetErrorsOrEmpty());
        }

        /// <summary>
        /// Инициализация одной ошибкой
        /// </summary>
        [Fact]
        public void Initialize_Error_HasOne()
        {
            var resultError = CreateErrorTest().ToRUnit();

            Assert.False(resultError.Success);
            Assert.True(resultError.Failure);
            Assert.Single(resultError.GetErrors());
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void AppendError()
        {
            var resultErrorInitial = RUnitFactory.Some();
            var errorToConcat = CreateErrorTest();

            var resultErrorConcat = resultErrorInitial.AppendError(errorToConcat);

            Assert.True(resultErrorConcat.Failure);
            Assert.Equal(1, resultErrorConcat.GetErrors().Count);
            Assert.True(errorToConcat.Equals(resultErrorConcat.GetErrors().Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalOne()
        {
            var resultErrorInitial = RUnitFactory.Some();
            var errorToConcat = CreateErrorTest();

            var resultErrorConcat = resultErrorInitial.ConcatErrors(errorToConcat);

            Assert.True(resultErrorConcat.Failure);
            Assert.Equal(1, resultErrorConcat.GetErrors().Count);
            Assert.True(errorToConcat.Equals(resultErrorConcat.GetErrors().Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var errorToConcat = CreateErrorTest();
            var resultErrorInitial = errorToConcat.ToRUnit();

            var resultErrorConcat = resultErrorInitial.ConcatErrors(errorToConcat);

            Assert.True(resultErrorConcat.Failure);
            Assert.Equal(2, resultErrorConcat.GetErrors().Count);
            Assert.True(errorToConcat.Equals(resultErrorConcat.GetErrors().Last()));
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
            const string value = "value";
            var valueNotFoundError = RErrorFactory.ValueNotFound<string>(nameof(value), value);
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");
            var errors = new List<IRError> { valueNotFoundError, databaseTableError };
            var result = errors.ToRUnit();

            var errorTypes = result.GetErrorTypes();

            Assert.True(errorTypes.SequenceEqual(errors.Select(error => error.GetType())));
        }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        [Fact]
        public void HasErrorType()
        {
            const string value = "value";
            var valueNotFoundError = RErrorFactory.ValueNotFound<string>(nameof(value), value);
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");
            var errors = new List<IRError> { valueNotFoundError, databaseTableError };
            var result = errors.ToRUnit();

            Assert.True(result.HasErrorType<DatabaseErrorType>());
        }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        [Fact]
        public void HasErrorTypeByType()
        {
            const string value = "value";
            var valueNotFoundError = RErrorFactory.ValueNotFound<string>(nameof(value), value);
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");
            var errors = new List<IRError> { valueNotFoundError, databaseTableError };
            var result = errors.ToRUnit();

            Assert.True(result.HasErrorType(CommonErrorType.ValueNotFound));
            Assert.False(result.HasErrorType(CommonErrorType.ValueNotValid));
        }

        /// <summary>
        /// Получить тип ошибки
        /// </summary>
        [Fact]
        public void GetErrorType()
        {
            const string value = "value";
            var valueNotFoundError = RErrorFactory.ValueNotFound<string>(nameof(value), value);
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");
            var errors = new List<IRError> { valueNotFoundError, databaseTableError };
            var result = errors.ToRUnit();

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
            const string value = "value";
            var valueNotFoundError = RErrorFactory.ValueNotFound<string>(nameof(value), value);
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");
            var errors = new List<IRError> { valueNotFoundError, databaseTableError };
            var result = errors.ToRUnit();

            var errorTypes = result.GetErrorsByTypes<DatabaseErrorType>();
            Assert.Equal(1, errorTypes.Count);
            Assert.Equal(DatabaseErrorType.TableAccess, errorTypes.First().ErrorType);
        }
    }
}