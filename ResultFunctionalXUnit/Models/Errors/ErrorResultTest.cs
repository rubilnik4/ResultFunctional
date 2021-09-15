using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Errors.DatabaseErrors;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.Models.Errors
{
    /// <summary>
    /// Ошибка результирующего ответа. Тесты
    /// </summary>
    public class ErrorResultTest
    {
        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        [Fact]
        public void HasErrorTypeCommon()
        {
            var errorResult = ErrorResultFactory.CommonError(CommonErrorType.Unknown, "Неизвестная ошибка");

            bool hasType = errorResult.HasErrorType<CommonErrorType>();

            Assert.True(hasType);
        }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        [Fact]
        public void HasErrorTypeDatabase()
        {
            var errorResult = ErrorResultFactory.DatabaseValueNotValidError("Test", "TestTable", "Неизвестная ошибка");

            bool hasType = errorResult.HasErrorType<DatabaseErrorType>();

            Assert.True(hasType);
        }

        /// <summary>
        /// Преобразование в строку
        /// </summary>
        [Fact]
        public void ToStringCommon()
        {
            var errorResult = ErrorResultFactory.CommonError(CommonErrorType.Unknown, "Неизвестная ошибка");

            var error = errorResult.ToString();

            Assert.Equal(CommonErrorType.Unknown.ToString(), error);
        }

        /// <summary>
        /// Вернуть ответ с одной ошибкой
        /// </summary>
        [Fact]
        public void ToResult_HasOneError()
        {
            var error = CreateErrorTest();

            var resultError = error.ToResult();

            Assert.True(resultError.HasErrors);
            Assert.Equal(1, resultError.Errors.Count);
        }

        /// <summary>
        /// Вернуть результирующий ответ с одной ошибкой
        /// </summary>
        [Fact]
        public void ToResultValue_HasStringType_HasOneError()
        {
            var error = CreateErrorTest();

            var resultValue = error.ToResultValue<string>();

            Assert.True(resultValue.HasErrors);
            Assert.Equal(1, resultValue.Errors.Count);
        }

        /// <summary>
        /// Вернуть результирующий ответ с одной ошибкой
        /// </summary>
        [Fact]
        public void ToResultCollection_HasStringType_HasOneError()
        {
            var error = CreateErrorTest();

            var resultCollection = error.ToResultCollection<string>();

            Assert.True(resultCollection.HasErrors);
            Assert.Equal(1, resultCollection.Errors.Count);
        }

        /// <summary>
        /// Преобразование в множество с одним элементом
        /// </summary>
        [Fact]
        public void IsEnumerableType_HasOne()
        {
            var error = CreateErrorTest();

            Assert.Single(error);
        }

        /// <summary>
        /// Преобразование в строку выводит тип ошибки
        /// </summary>
        [Fact]
        public void ToStringFormatIsErrorType()
        {
            var error = CreateErrorTest();

            Assert.IsType<ErrorTypeResult<TestErrorType>>(error);
            Assert.Equal(((ErrorTypeResult<TestErrorType>)error).ErrorType.ToString(), error.ToString());
        }

        /// <summary>
        /// Проверка добавления ошибки
        /// </summary>
        [Fact]
        public void AppendException()
        {
            var error = CreateErrorTest();
            var exception = new ArgumentException();

            var errorWithException = error.AppendException(exception);

            Assert.True(exception.Equals(errorWithException.Exception));
        }

        /// <summary>
        /// Наличие специфической ошибки
        /// </summary>
        [Fact]
        public void HasError()
        {
            var databaseTableError = ErrorResultFactory.DatabaseAccessError("TestTable", "error");

            Assert.True(databaseTableError.HasErrorResult<IDatabaseAccessErrorResult>());
            Assert.True(databaseTableError.HasErrorResult<DatabaseAccessErrorResult>());
        }

        /// <summary>
        /// Наличие специфической ошибки
        /// </summary>
        [Fact]
        public void IsError()
        {
            var databaseTableError = ErrorResultFactory.DatabaseAccessError("TestTable", "error");

            Assert.False(databaseTableError.IsErrorResult<IDatabaseAccessErrorResult>());
            Assert.True(databaseTableError.IsErrorResult<DatabaseAccessErrorResult>());
        }
    }
}