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
    public class ResultValueBindTryWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkTaskAsync_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = RValueFactory.SomeTask(initialValue);

            var numberAfterTry = await numberResult.ResultValueBindTryOkTaskAsync(
                numbers => new ResultValue<int>(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkTaskAsync_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueBindTryOkTaskAsync(
                numbers => new ResultValue<int>(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkTaskAsync_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = RValueFactory.SomeTask(initialNumber);

            var resultValue = await numberResult.ResultValueBindTryOkTaskAsync(
                numbers => new ResultValue<int>(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkTaskAsync_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueBindTryOkTaskAsync(
                numbers => new ResultValue<int>(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkTaskAsyncFunc_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = RValueFactory.SomeTask(initialValue);

            var numberAfterTry = await numberResult.ResultValueBindTryOkTaskAsync(
                numbers => new ResultValue<int>(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkTaskAsyncFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueBindTryOkTaskAsync(
                numbers => new ResultValue<int>(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkTaskAsyncFunc_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = RValueFactory.SomeTask(initialNumber);

            var resultValue = await numberResult.ResultValueBindTryOkTaskAsync(
                numbers => new ResultValue<int>(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryOkTaskAsyncFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueBindTryOkTaskAsync(
                numbers => new ResultValue<int>(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}