using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений для задачи-объекта. Тесты
    /// </summary>
    public class ResultValueTryWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsync_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = RValueFactory.SomeTask(initialValue);

            var numberAfterTry = await numberResult.ResultValueTryOkTaskAsync(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(Division(initialValue), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsync_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueTryOkTaskAsync(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsync_OkResult_ExceptionTry()
        {
            const int initialValue = 0;
            var numberResult = RValueFactory.SomeTask(initialValue);

            var resultValue = await numberResult.ResultValueTryOkTaskAsync(Division, Exceptions.ExceptionError());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsync_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueTryOkTaskAsync(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsyncFunc_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = RValueFactory.SomeTask(initialValue);

            var numberAfterTry = await numberResult.ResultValueTryOkTaskAsync(Division, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(Division(initialValue), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsyncFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueTryOkTaskAsync(Division, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsyncFunc_OkResult_ExceptionTry()
        {
            const int initialValue = 0;
            var numberResult = RValueFactory.SomeTask(initialValue);

            var resultValue = await numberResult.ResultValueTryOkTaskAsync(Division, Exceptions.ExceptionFunc());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkTaskAsyncFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.ResultValueTryOkTaskAsync(Division, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}