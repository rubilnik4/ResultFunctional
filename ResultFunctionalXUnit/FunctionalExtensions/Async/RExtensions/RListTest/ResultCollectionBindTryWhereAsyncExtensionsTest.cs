using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.AsyncFunctions;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class RListBindTryWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Асинхронный положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RListBindTryAsyncOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = initialNumbers.ToRList();

            var numbersAfterTry = await numbersResult.RListBindTrySomeAsync(
                numbers => RListFactory.SomeTask(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.Success);
            Assert.True((await DivisionByCollectionAsync(initialNumbers)).SequenceEqual(numbersAfterTry.GetValue()));
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RListBindTryAsyncOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numbersAfterTry = await numbersResult.RListBindTrySomeAsync(
               numbers => RListFactory.SomeTask(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.Failure);
            Assert.True(initialError.Equals(numbersAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task RListBindTryAsyncOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numbersResult = initialNumbers.ToRList();

            var numbersAfterTry = await numbersResult.RListBindTrySomeAsync(
                 numbers => RListFactory.SomeTask(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.Failure);
            Assert.NotNull(numbersAfterTry.GetErrors().First().Exception);
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task RListBindTryAsyncOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = await numbersResult.RListBindTrySomeAsync(
                 numbers => RListFactory.SomeTask(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RListBindTryAsyncOkFunc_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = initialNumbers.ToRList();

            var numbersAfterTry = await numbersResult.RListBindTrySomeAsync(
                numbers => RListFactory.SomeTask(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.Success);
            Assert.True((await DivisionByCollectionAsync(initialNumbers)).SequenceEqual(numbersAfterTry.GetValue()));
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RListBindTryAsyncOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numbersAfterTry = await numbersResult.RListBindTrySomeAsync(
               numbers => RListFactory.SomeTask(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.Failure);
            Assert.True(initialError.Equals(numbersAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task RListBindTryAsyncOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numbersResult = initialNumbers.ToRList();

            var numbersAfterTry = await numbersResult.RListBindTrySomeAsync(
                 numbers => RListFactory.SomeTask(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.Failure);
            Assert.NotNull(numbersAfterTry.GetErrors().First().Exception);
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task RListBindTryAsyncOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = await numbersResult.RListBindTrySomeAsync(
                 numbers => RListFactory.SomeTask(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}