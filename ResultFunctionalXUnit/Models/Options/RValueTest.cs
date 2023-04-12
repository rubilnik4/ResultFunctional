using System;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Errors.BaseErrors;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.Models.Options
{
    /// <summary>
    /// Базовый вариант ответа со значением. Тест
    /// </summary>
    public class RValueTest
    {
        /// <summary>
        /// Инициализация с ошибкой
        /// </summary>
        [Fact]
        public void Initialize_OneError()
        {
            var resultValue = CreateErrorTest().ToRValue<string>();

            Assert.False(resultValue.Success);
            Assert.True(resultValue.Failure);
            Assert.Single(resultValue.GetErrors());
            Assert.Throws<ArgumentNullException>(resultValue.GetValue);
            Assert.Null(resultValue.Value);
        }

        /// <summary>
        /// Инициализация со списком ошибок
        /// </summary>
        [Fact]
        public void Initialize_Errors()
        {
            var errors = CreateErrorListTwoTest();
            var resultValue = errors.ToRValue<string>();

            Assert.False(resultValue.Success);
            Assert.True(resultValue.Failure);
            Assert.Equal(errors.Count, resultValue.GetErrors().Count);
        }

        /// <summary>
        /// Инициализация со значением
        /// </summary>
        [Fact]
        public void Initialize_HasValue()
        {
            var resultValue = "OK".ToRValue();

            Assert.True(resultValue.Success);
            Assert.False(resultValue.Failure);
            Assert.Empty(resultValue.GetErrorsOrEmpty());
            Assert.True(resultValue.GetValue() != null);
        }

        /// <summary>
        /// Добавление ошибки
        /// </summary>
        [Fact]
        public void AppendError()
        {
            const string value = "OK";
            var resultValueInitial = value.ToRValue();
            var errorToConcat = CreateErrorTest();

            var resultValueConcat = resultValueInitial.AppendError(errorToConcat);

            Assert.True(resultValueConcat.Failure);
            Assert.Single(resultValueConcat.GetErrors());
            Assert.True(errorToConcat.Equals(resultValueConcat.GetErrors().Last()));
            Assert.Null(resultValueConcat.Value);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalOne()
        {
            const string value = "OK";
            var resultValueInitial = value.ToRValue();
            var errorToConcat = CreateErrorTest();

            var resultValueConcat = resultValueInitial.ConcatErrors(errorToConcat);

            Assert.True(resultValueConcat.Failure);
            Assert.Single(resultValueConcat.GetErrors());
            Assert.True(errorToConcat.Equals(resultValueConcat.GetErrors().Last()));
            Assert.Null(resultValueConcat.Value);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var initialError = CreateErrorTest();
            var resultValueInitial = initialError.ToRValue<string>();
            var errorToConcat = CreateErrorTest();

            var resultValueConcat = resultValueInitial.ConcatErrors(errorToConcat);

            Assert.True(resultValueConcat.Failure);
            Assert.Equal(2, resultValueConcat.GetErrors().Count);
            Assert.True(initialError.Equals(resultValueConcat.GetErrors().First()));
            Assert.Null(resultValueConcat.Value);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта без ошибок при отправке пустого списка
        /// </summary>
        [Fact]
        public void ConcatErrors_OkStatus_EmptyList()
        {
            const string value = "OK";
            var resultValueInitial = value.ToRValue();
            var errorsToConcat = Enumerable.Empty<IRError>();

            var resultValueConcat = resultValueInitial.ConcatErrors(errorsToConcat);

            Assert.True(resultValueConcat.Success);
            Assert.Equal(value, resultValueConcat.GetValue());
        }
    }
}