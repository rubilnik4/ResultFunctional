using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.RExtension.Lists.ResultCollectionTryAsyncExtensions;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.AsyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultCollectionTryWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Асинхронный положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = initialNumbers.ToRList();

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkAsync(DivisionByCollectionAsync, Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.Success);
            Assert.True((await DivisionByCollectionAsync(initialNumbers)).SequenceEqual(numbersAfterTry.GetValue()));
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkAsync(DivisionByCollectionAsync, Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.Failure);
            Assert.True(initialError.Equals(numbersAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumber();
            var numberResult = initialNumbers.ToRList();

            var resultValue = await numberResult.ResultCollectionTryOkAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionError());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = await numbersResult.ResultCollectionTryOkAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOkFunc_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = initialNumbers.ToRList();

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkAsync(DivisionByCollectionAsync, Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.Success);
            Assert.True((await DivisionByCollectionAsync(initialNumbers)).SequenceEqual(numbersAfterTry.GetValue()));
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkAsync(DivisionByCollectionAsync, Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.Failure);
            Assert.True(initialError.Equals(numbersAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumber();
            var numberResult = initialNumbers.ToRList();

            var resultValue = await numberResult.ResultCollectionTryOkAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionFunc());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = await numbersResult.ResultCollectionTryOkAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}