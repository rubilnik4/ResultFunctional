using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues.ResultValueTryAsyncExtensions;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultValueTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultValueTryAsync_Ok()
        {
            int initialValue = Numbers.Number;
            var resultValue = await ResultValueTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), resultValue.Value);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueTryAsync_Exception()
        {
            const int initialValue = 0;
            var resultValue = await ResultValueTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkAsync_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = new ResultValue<int>(initialValue);

            var numberAfterTry = await numberResult.ResultValueTryOkAsync(AsyncFunctions.DivisionAsync, CreateErrorTest());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkAsync_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueTryOkAsync(AsyncFunctions.DivisionAsync, CreateErrorTest());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkAsync_OkResult_ExceptionTry()
        {
            const int initialValue = 0;
            var numberResult = new ResultValue<int>(initialValue);

            var resultValue = await numberResult.ResultValueTryOkAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkAsync_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueTryOkAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}