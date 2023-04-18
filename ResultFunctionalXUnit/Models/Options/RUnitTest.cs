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
            var rMaybe = RUnitFactory.Some();

            Assert.True(rMaybe.Success);
            Assert.False(rMaybe.Failure);
            Assert.Null(rMaybe.Errors);
            Assert.Throws<ArgumentNullException>(rMaybe.GetErrors);
            Assert.Empty(rMaybe.GetErrorsOrEmpty());
        }

        /// <summary>
        /// Инициализация одной ошибкой
        /// </summary>
        [Fact]
        public void Initialize_Error_HasOne()
        {
            var rMaybe = CreateErrorTest().ToRUnit();

            Assert.False(rMaybe.Success);
            Assert.True(rMaybe.Failure);
            Assert.Single(rMaybe.GetErrors());
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void AppendError()
        {
            var rMaybeInitial = RUnitFactory.Some();
            var errorToConcat = CreateErrorTest();

            var rMaybeConcat = rMaybeInitial.AppendError(errorToConcat);

            Assert.True(rMaybeConcat.Failure);
            Assert.Equal(1, rMaybeConcat.GetErrors().Count);
            Assert.True(errorToConcat.Equals(rMaybeConcat.GetErrors().Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalOne()
        {
            var rMaybeInitial = RUnitFactory.Some();
            var errorToConcat = CreateErrorTest();

            var rMaybeConcat = rMaybeInitial.ConcatErrors(errorToConcat);

            Assert.True(rMaybeConcat.Failure);
            Assert.Equal(1, rMaybeConcat.GetErrors().Count);
            Assert.True(errorToConcat.Equals(rMaybeConcat.GetErrors().Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var errorToConcat = CreateErrorTest();
            var rMaybeInitial = errorToConcat.ToRUnit();

            var rMaybeConcat = rMaybeInitial.ConcatErrors(errorToConcat);

            Assert.True(rMaybeConcat.Failure);
            Assert.Equal(2, rMaybeConcat.GetErrors().Count);
            Assert.True(errorToConcat.Equals(rMaybeConcat.GetErrors().Last()));
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