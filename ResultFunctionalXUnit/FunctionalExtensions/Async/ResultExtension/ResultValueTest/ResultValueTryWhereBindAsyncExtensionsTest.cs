using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений для задачи-объекта. Тесты
    /// </summary>
    public class ResultValueTryWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkBindAsync_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = ResultValueFactory.CreateTaskResultValue(initialValue);

            var numberAfterTry = await numberResult.ResultValueTryOkBindAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(Division(initialValue), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkBindAsync_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = ResultValueFactory.CreateTaskResultValueError<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueTryOkBindAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkBindAsync_OkResult_ExceptionTry()
        {
            const int initialValue = 0;
            var numberResult = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultValue = await numberResult.ResultValueTryOkBindAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkBindAsync_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = ResultValueFactory.CreateTaskResultValueError<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueTryOkBindAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkBindAsyncFunc_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = ResultValueFactory.CreateTaskResultValue(initialValue);

            var numberAfterTry = await numberResult.ResultValueTryOkBindAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.OkStatus);
            Assert.Equal(Division(initialValue), numberAfterTry.Value);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkBindAsyncFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = ResultValueFactory.CreateTaskResultValueError<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueTryOkBindAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkBindAsyncFunc_OkResult_ExceptionTry()
        {
            const int initialValue = 0;
            var numberResult = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultValue = await numberResult.ResultValueTryOkBindAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionFunc());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkBindAsyncFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = ResultValueFactory.CreateTaskResultValueError<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueTryOkBindAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}