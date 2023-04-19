using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.AsyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class RListTryOptionAsyncExtensionsTest
    {
        /// <summary>
        /// Асинхронный положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RListTryAsyncOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = initialNumbers.ToRList();

            var numbersAfterTry = await numbersResult.RListTrySomeAsync(DivisionByCollectionAsync, Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.Success);
            Assert.True((await DivisionByCollectionAsync(initialNumbers)).SequenceEqual(numbersAfterTry.GetValue()));
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RListTryAsyncOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numbersAfterTry = await numbersResult.RListTrySomeAsync(DivisionByCollectionAsync, Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.Failure);
            Assert.True(initialError.Equals(numbersAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task RListTryAsyncOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumber();
            var numberResult = initialNumbers.ToRList();

            var rValue = await numberResult.RListTrySomeAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionError());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task RListTryAsyncOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = await numbersResult.RListTrySomeAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RListTryAsyncOkFunc_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = initialNumbers.ToRList();

            var numbersAfterTry = await numbersResult.RListTrySomeAsync(DivisionByCollectionAsync, Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.Success);
            Assert.True((await DivisionByCollectionAsync(initialNumbers)).SequenceEqual(numbersAfterTry.GetValue()));
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RListTryAsyncOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numbersAfterTry = await numbersResult.RListTrySomeAsync(DivisionByCollectionAsync, Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.Failure);
            Assert.True(initialError.Equals(numbersAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task RListTryAsyncOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumber();
            var numberResult = initialNumbers.ToRList();

            var rValue = await numberResult.RListTrySomeAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionFunc());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task RListTryAsyncOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = initialError.ToRList<int>();

            var numberAfterTry = await numbersResult.RListTrySomeAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}