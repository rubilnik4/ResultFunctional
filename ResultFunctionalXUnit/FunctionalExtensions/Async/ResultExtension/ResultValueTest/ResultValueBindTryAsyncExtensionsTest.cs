using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues.ResultValueBindTryAsyncExtensions;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultValueBindTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryAsync_Ok()
        {
            int initialValue = Numbers.Number;

            var resultValue = await ResultValueBindTryAsync(() => ResultValueFactory.CreateTaskResultValue(Division(initialValue)),
                                                            Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(resultValue.Value, await AsyncFunctions.DivisionAsync(initialValue));
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryAsync_Exception()
        {
            const int initialValue = 0;

            var resultValue = await ResultValueBindTryAsync(() => ResultValueFactory.CreateTaskResultValue(Division(initialValue)),
                                                            Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkAsync_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = new ResultValue<int>(initialValue);

            var numberAfterTry = await numberResult.ResultValueBindTryOkAsync(
                numbers => ResultValueFactory.CreateTaskResultValue(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkAsync_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueBindTryOkAsync(
                numbers => ResultValueFactory.CreateTaskResultValue(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkAsync_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = new ResultValue<int>(initialNumber);

            var resultValue = await numberResult.ResultValueBindTryOkAsync(
                numbers => ResultValueFactory.CreateTaskResultValue(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkAsync_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = new ResultValue<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueBindTryOkAsync(
                numbers => ResultValueFactory.CreateTaskResultValue(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}