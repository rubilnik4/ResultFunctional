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
            var rValue = CreateErrorTest().ToRValue<string>();

            Assert.False(rValue.Success);
            Assert.True(rValue.Failure);
            Assert.Single(rValue.GetErrors());
            Assert.Throws<ArgumentNullException>(rValue.GetValue);
            Assert.Null(rValue.Value);
        }

        /// <summary>
        /// Инициализация со списком ошибок
        /// </summary>
        [Fact]
        public void Initialize_Errors()
        {
            var errors = CreateErrorListTwoTest();
            var rValue = errors.ToRValue<string>();

            Assert.False(rValue.Success);
            Assert.True(rValue.Failure);
            Assert.Equal(errors.Count, rValue.GetErrors().Count);
        }

        /// <summary>
        /// Инициализация со значением
        /// </summary>
        [Fact]
        public void Initialize_HasValue()
        {
            var rValue = "OK".ToRValue();

            Assert.True(rValue.Success);
            Assert.False(rValue.Failure);
            Assert.Empty(rValue.GetErrorsOrEmpty());
            Assert.True(rValue.GetValue() != null);
        }

        /// <summary>
        /// Инициализация со значением
        /// </summary>
        [Fact]
        public void Initialize_Exception()
        {
            string empty = null!;

            Assert.Throws<ArgumentNullException>(empty.ToRValue);
        }

        /// <summary>
        /// Добавление ошибки
        /// </summary>
        [Fact]
        public void AppendError()
        {
            const string value = "OK";
            var rValueInitial = value.ToRValue();
            var errorToConcat = CreateErrorTest();

            var rValueConcat = rValueInitial.AppendError(errorToConcat);

            Assert.True(rValueConcat.Failure);
            Assert.Single(rValueConcat.GetErrors());
            Assert.True(errorToConcat.Equals(rValueConcat.GetErrors().Last()));
            Assert.Null(rValueConcat.Value);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalOne()
        {
            const string value = "OK";
            var rValueInitial = value.ToRValue();
            var errorToConcat = CreateErrorTest();

            var rValueConcat = rValueInitial.ConcatErrors(errorToConcat);

            Assert.True(rValueConcat.Failure);
            Assert.Single(rValueConcat.GetErrors());
            Assert.True(errorToConcat.Equals(rValueConcat.GetErrors().Last()));
            Assert.Null(rValueConcat.Value);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var initialError = CreateErrorTest();
            var rValueInitial = initialError.ToRValue<string>();
            var errorToConcat = CreateErrorTest();

            var rValueConcat = rValueInitial.ConcatErrors(errorToConcat);

            Assert.True(rValueConcat.Failure);
            Assert.Equal(2, rValueConcat.GetErrors().Count);
            Assert.True(initialError.Equals(rValueConcat.GetErrors().First()));
            Assert.Null(rValueConcat.Value);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта без ошибок при отправке пустого списка
        /// </summary>
        [Fact]
        public void ConcatErrors_OkStatus_EmptyList()
        {
            const string value = "OK";
            var rValueInitial = value.ToRValue();
            var errorsToConcat = Enumerable.Empty<IRError>();

            var rValueConcat = rValueInitial.ConcatErrors(errorsToConcat);

            Assert.True(rValueConcat.Success);
            Assert.Equal(value, rValueConcat.GetValue());
        }
    }
}