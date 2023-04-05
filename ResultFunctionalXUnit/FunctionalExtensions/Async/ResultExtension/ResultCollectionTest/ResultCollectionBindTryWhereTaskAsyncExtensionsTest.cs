using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.RExtension.Lists.ResultCollectionTryAsyncExtensions;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.AsyncFunctions;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultCollectionBindTryWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Асинхронный положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryTaskAsyncOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = RListFactory.SomeTask(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionBindTryOkTaskAsync(
                numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.OkStatus);
            Assert.True((await DivisionByCollectionAsync(initialNumbers)).SequenceEqual(numbersAfterTry.Value));
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryTaskAsyncOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = RListFactory.NoneTask<int>(initialError);

            var numbersAfterTry = await numbersResult.ResultCollectionBindTryOkTaskAsync(
               numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.True(initialError.Equals(numbersAfterTry.Errors.First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryTaskAsyncOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numbersResult = RListFactory.SomeTask(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionBindTryOkTaskAsync(
                 numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.NotNull(numbersAfterTry.Errors.First().Exception);
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryTaskAsyncOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = RListFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numbersResult.ResultCollectionBindTryOkTaskAsync(
                 numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryTaskAsyncOkFunc_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = RListFactory.SomeTask(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionBindTryOkTaskAsync(
                numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.OkStatus);
            Assert.True((await DivisionByCollectionAsync(initialNumbers)).SequenceEqual(numbersAfterTry.Value));
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryTaskAsyncOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = RListFactory.NoneTask<int>(initialError);

            var numbersAfterTry = await numbersResult.ResultCollectionBindTryOkTaskAsync(
               numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.True(initialError.Equals(numbersAfterTry.Errors.First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryTaskAsyncOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumberWithZero();
            var numbersResult = RListFactory.SomeTask(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionBindTryOkTaskAsync(
                 numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.NotNull(numbersAfterTry.Errors.First().Exception);
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionBindTryTaskAsyncOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = RListFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numbersResult.ResultCollectionBindTryOkTaskAsync(
                 numbers => new ResultCollection<int>(DivisionByCollection(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}