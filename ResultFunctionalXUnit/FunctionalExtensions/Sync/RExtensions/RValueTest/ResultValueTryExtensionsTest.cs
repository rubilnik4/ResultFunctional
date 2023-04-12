﻿using System.Linq;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;
using static ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values.RValueTryExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений. Тесты
    /// </summary>
    public class ResultValueTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void ResultValueTry_Ok()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueTry(() => Division(initialValue), Exceptions.ExceptionError());

            Assert.True(resultValue.Success);
            Assert.Equal(Division(initialValue), resultValue.GetValue());
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueTry_Exception()
        {
            const int initialValue = 0;
            var resultValue = RValueTry(() => Division(initialValue), Exceptions.ExceptionError());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void ResultValueTryFunc_Ok()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueTry(() => Division(initialValue), Exceptions.ExceptionFunc());

            Assert.True(resultValue.Success);
            Assert.Equal(Division(initialValue), resultValue.GetValue());
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueTryFunc_Exception()
        {
            const int initialValue = 0;
            var resultValue = RValueTry(() => Division(initialValue), Exceptions.ExceptionFunc());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }
    }
}