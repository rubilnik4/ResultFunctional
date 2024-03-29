﻿using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с со связыванием коллекцией и обработкой исключений. Тесты
    /// </summary>
    public class RListBindTryOptionExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public void RListBindTryOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = initialNumbers.ToRList();

            var numberAfterTry = numbersResult.RListBindTrySome(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Success);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.GetValue()));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void RListBindTryOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = numbersResult.RListBindTrySome(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void RListBindTryOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = initialNumbers.ToRList();

            var rValue = numberResult.RListBindTrySome(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionError());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void RListBindTryOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRList<int>();

            var numberAfterTry = numberResult.RListBindTrySome(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public void RListBindTryOkFunc_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = initialNumbers.ToRList();

            var numberAfterTry = numbersResult.RListBindTrySome(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Success);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numberAfterTry.GetValue()));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public void RListBindTryOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = numbersResult.RListBindTrySome(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public void RListBindTryOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numberResult = initialNumbers.ToRList();

            var rValue = numberResult.RListBindTrySome(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionFunc());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public void RListBindTryOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRList<int>();

            var numberAfterTry = numberResult.RListBindTrySome(
                numbers => DivisionByCollection(numbers).ToRList(), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}