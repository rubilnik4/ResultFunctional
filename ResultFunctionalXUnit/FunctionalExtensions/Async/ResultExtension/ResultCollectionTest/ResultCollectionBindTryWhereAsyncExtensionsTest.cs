using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.AsyncFunctions;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultCollectionBindTryWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Асинхронный положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryAsyncOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = new ResultCollection<int>(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionBindTryOkAsync(
                numbers => RListFactory.GetRListAsync(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.OkStatus);
            Assert.True((await DivisionByCollectionAsync(initialNumbers)).SequenceEqual(numbersAfterTry.Value));
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryAsyncOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numbersAfterTry = await numbersResult.ResultCollectionBindTryOkAsync(
               numbers => RListFactory.GetRListAsync(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.True(initialError.Equals(numbersAfterTry.Errors.First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryAsyncOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numbersResult = new ResultCollection<int>(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionBindTryOkAsync(
                 numbers => RListFactory.GetRListAsync(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.NotNull(numbersAfterTry.Errors.First().Exception);
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryAsyncOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numberAfterTry = await numbersResult.ResultCollectionBindTryOkAsync(
                 numbers => RListFactory.GetRListAsync(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryAsyncOkFunc_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = new ResultCollection<int>(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionBindTryOkAsync(
                numbers => RListFactory.GetRListAsync(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.OkStatus);
            Assert.True((await DivisionByCollectionAsync(initialNumbers)).SequenceEqual(numbersAfterTry.Value));
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryAsyncOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numbersAfterTry = await numbersResult.ResultCollectionBindTryOkAsync(
               numbers => RListFactory.GetRListAsync(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.True(initialError.Equals(numbersAfterTry.Errors.First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryAsyncOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numbersResult = new ResultCollection<int>(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionBindTryOkAsync(
                 numbers => RListFactory.GetRListAsync(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.NotNull(numbersAfterTry.Errors.First().Exception);
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryAsyncOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numberAfterTry = await numbersResult.ResultCollectionBindTryOkAsync(
                 numbers => RListFactory.GetRListAsync(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}