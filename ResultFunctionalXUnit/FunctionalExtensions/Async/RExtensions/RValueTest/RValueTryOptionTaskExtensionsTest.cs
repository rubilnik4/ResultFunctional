using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений для задачи-объекта. Тесты
    /// </summary>
    public class RValueTryOptionTaskExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RValueTryOkTaskAsync_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = RValueFactory.SomeTask(initialValue);

            var numberAfterTry = await numberResult.RValueTrySomeTask(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(Division(initialValue), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RValueTryOkTaskAsync_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.RValueTrySomeTask(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RValueTryOkTaskAsync_OkResult_ExceptionTry()
        {
            const int initialValue = 0;
            var numberResult = RValueFactory.SomeTask(initialValue);

            var rValue = await numberResult.RValueTrySomeTask(Division, Exceptions.ExceptionError());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RValueTryOkTaskAsync_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.RValueTrySomeTask(Division, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RValueTryOkTaskAsyncFunc_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = RValueFactory.SomeTask(initialValue);

            var numberAfterTry = await numberResult.RValueTrySomeTask(Division, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(Division(initialValue), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RValueTryOkTaskAsyncFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.RValueTrySomeTask(Division, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RValueTryOkTaskAsyncFunc_OkResult_ExceptionTry()
        {
            const int initialValue = 0;
            var numberResult = RValueFactory.SomeTask(initialValue);

            var rValue = await numberResult.RValueTrySomeTask(Division, Exceptions.ExceptionFunc());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RValueTryOkTaskAsyncFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.RValueTrySomeTask(Division, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}