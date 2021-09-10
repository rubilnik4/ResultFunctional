using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.Models.Results
{
    /// <summary>
    /// Базовый вариант ответа со значением. Тест
    /// </summary>
    public class ResultValueTest
    {
        /// <summary>
        /// Инициализация с ошибкой
        /// </summary>
        [Fact]
        public void Initialize_OneError()
        {
            var resultValue = new ResultValue<string>(CreateErrorTest());

            Assert.False(resultValue.OkStatus);
            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
        }

        /// <summary>
        /// Инициализация со списком ошибок
        /// </summary>
        [Fact]
        public void Initialize_Errors()
        {
            var errors = CreateErrorListTwoTest();
            var resultValue = new ResultValue<string>(errors);

            Assert.False(resultValue.OkStatus);
            Assert.True(resultValue.HasErrors);
            Assert.Equal(errors.Count, resultValue.Errors.Count);
        }

        /// <summary>
        /// Инициализация со значением
        /// </summary>
        [Fact]
        public void Initialize_HasValue()
        {
            var resultValue = new ResultValue<string>("OK");

            Assert.True(resultValue.OkStatus);
            Assert.False(resultValue.HasErrors);
            Assert.Empty(resultValue.Errors);
            Assert.True(resultValue.Value != null);
        }

        /// <summary>
        /// Добавление ошибки
        /// </summary>
        [Fact]
        public void AppendError()
        {
            const string value = "OK";
            var resultValueInitial = new ResultValue<string>(value);
            var errorToConcat = CreateErrorTest();

            var resultValueConcat = resultValueInitial.AppendError(errorToConcat);

            Assert.True(resultValueConcat.HasErrors);
            Assert.Single(resultValueConcat.Errors);
            Assert.True(errorToConcat.Equals(resultValueConcat.Errors.Last()));
            Assert.Null(resultValueConcat.Value);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalOne()
        {
            const string value = "OK";
            var resultValueInitial = new ResultValue<string>(value);
            var errorToConcat = CreateErrorTest();

            var resultValueConcat = resultValueInitial.ConcatErrors(errorToConcat);

            Assert.True(resultValueConcat.HasErrors);
            Assert.Single(resultValueConcat.Errors);
            Assert.True(errorToConcat.Equals(resultValueConcat.Errors.Last()));
            Assert.Null(resultValueConcat.Value);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var initialError = CreateErrorTest();
            var resultValueInitial = new ResultValue<string>(initialError);
            var errorToConcat = CreateErrorTest();

            var resultValueConcat = resultValueInitial.ConcatErrors(errorToConcat);

            Assert.True(resultValueConcat.HasErrors);
            Assert.Equal(2, resultValueConcat.Errors.Count);
            Assert.True(initialError.Equals(resultValueConcat.Errors.First()));
            Assert.Null(resultValueConcat.Value);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта без ошибок при отправке пустого списка
        /// </summary>
        [Fact]
        public void ConcatErrors_OkStatus_EmptyList()
        {
            const string value = "OK";
            var resultValueInitial = new ResultValue<string>(value);
            var errorsToConcat = Enumerable.Empty<IErrorResult>();

            var resultValueConcat = resultValueInitial.ConcatErrors(errorsToConcat);

            Assert.True(resultValueConcat.OkStatus);
            Assert.Equal(value, resultValueConcat.Value);
        }

        /// <summary>
        /// Инициализация из базового класса
        /// </summary>
        [Fact]
        public void InitializeBaseResult_NoError()
        {
            const string value = "test";
            var resultError = new ResultValue<string>(value);
            var resultErrorType = resultError.ToResultValueType<IValueNotFoundErrorResult>();

            Assert.True(resultErrorType.OkStatus);
            Assert.IsAssignableFrom<IResultValueType<string, IValueNotFoundErrorResult>>(resultErrorType);
        }

        /// <summary>
        /// Инициализация из базового класса
        /// </summary>
        [Fact]
        public void InitializeBaseResult()
        {
            var valueError = ErrorResultFactory.ValueNotFoundError("test", GetType());
            var databaseError = ErrorResultFactory.DatabaseTableError("Table", "TableError");
            var errors = new List<IErrorResult> { valueError, databaseError };
            var resultError = new ResultValue<string>(errors);
            var resultErrorType = resultError.ToResultValueType<IValueNotFoundErrorResult>();

            Assert.True(resultErrorType.HasErrors);
            Assert.Equal(2, resultErrorType.Errors.Count);
            Assert.Single(resultErrorType.ErrorsByType);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(resultErrorType.Errors.First());
        }
    }
}