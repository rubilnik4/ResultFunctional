﻿using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class RValueBindTryOptionAsyncExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkAsync_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = initialValue.ToRValue();

            var numberAfterTry = await numberResult.RValueBindTrySomeAsync(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkAsync_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = await numberResult.RValueBindTrySomeAsync(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkAsync_OkResult_ExceptionTry()
        {
            const int initialValue = 0;
            var numberResult = initialValue.ToRValue();

            var rValue = await numberResult.RValueBindTrySomeAsync(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkAsync_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = await numberResult.RValueBindTrySomeAsync(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkAsyncFunc_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = initialValue.ToRValue();

            var numberAfterTry = await numberResult.RValueBindTrySomeAsync(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkAsyncFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = await numberResult.RValueBindTrySomeAsync(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkAsyncFunc_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = initialNumber.ToRValue();

            var rValue = await numberResult.RValueBindTrySomeAsync(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkAsyncFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = await numberResult.RValueBindTrySomeAsync(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}