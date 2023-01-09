using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultValueBindTryWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkBindAsync_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = ResultValueFactory.CreateTaskResultValue(initialValue);

            var numberAfterTry = await numberResult.ResultValueBindTryOkBindAsync(
                numbers => ResultValueFactory.CreateTaskResultValue(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkBindAsync_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = ResultValueFactory.CreateTaskResultValueError<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueBindTryOkBindAsync(
                numbers => ResultValueFactory.CreateTaskResultValue(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkBindAsync_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = ResultValueFactory.CreateTaskResultValue(initialNumber);

            var resultValue = await numberResult.ResultValueBindTryOkBindAsync(
                numbers => ResultValueFactory.CreateTaskResultValue(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkBindAsync_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = ResultValueFactory.CreateTaskResultValueError<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueBindTryOkBindAsync(
                numbers => ResultValueFactory.CreateTaskResultValue(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkBindAsyncFunc_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = ResultValueFactory.CreateTaskResultValue(initialValue);

            var numberAfterTry = await numberResult.ResultValueBindTryOkBindAsync(
                numbers => ResultValueFactory.CreateTaskResultValue(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkBindAsyncFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = ResultValueFactory.CreateTaskResultValueError<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueBindTryOkBindAsync(
                numbers => ResultValueFactory.CreateTaskResultValue(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkBindAsyncFunc_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = ResultValueFactory.CreateTaskResultValue(initialNumber);

            var resultValue = await numberResult.ResultValueBindTryOkBindAsync(
                numbers => ResultValueFactory.CreateTaskResultValue(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkBindAsyncFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = ResultValueFactory.CreateTaskResultValueError<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueBindTryOkBindAsync(
                numbers => ResultValueFactory.CreateTaskResultValue(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}