using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections.ResultCollectionTryAsyncExtensions;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.AsyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultCollectionTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Ok_IEnumerable()
        {
            var resultValue = await ResultCollectionTryAsync(() => DivisionEnumerableAsync(1), Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(await DivisionCollectionAsync(1), resultValue.Value);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Exception_IEnumerable()
        {
            var resultValue = await ResultCollectionTryAsync(() => DivisionEnumerableAsync(0), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Ok_IReadonlyCollection()
        {
            var resultValue = await ResultCollectionTryAsync(() => DivisionCollectionAsync(1), Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(await DivisionCollectionAsync(1), resultValue.Value);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Exception_IReadonlyCollection()
        {
            var resultValue = await ResultCollectionTryAsync(() => DivisionCollectionAsync(0), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Ok_List()
        {
            var resultValue = await ResultCollectionTryAsync(() => DivisionListAsync(1), Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(await DivisionCollectionAsync(1), resultValue.Value);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Exception_List()
        {
            var resultValue = await ResultCollectionTryAsync(() => DivisionListAsync(0), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = new ResultCollection<int>(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkAsync(DivisionByCollectionAsync, CreateErrorTest());

            Assert.True(numbersAfterTry.OkStatus);
            Assert.True((await DivisionByCollectionAsync(initialNumbers)).SequenceEqual(numbersAfterTry.Value));
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkAsync(DivisionByCollectionAsync, CreateErrorTest());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.True(initialError.Equals(numbersAfterTry.Errors.First()));
        }

        /// <summary>
        /// Асинхронный положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumber();
            var numberResult =new ResultCollection<int>(initialNumbers);

            var resultValue = await numberResult.ResultCollectionTryOkAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Асинхронный результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = new ResultCollection<int>(initialError);

            var numberAfterTry = await numbersResult.ResultCollectionTryOkAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}