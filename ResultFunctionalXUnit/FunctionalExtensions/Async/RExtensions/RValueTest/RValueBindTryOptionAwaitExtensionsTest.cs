using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class RValueBindTryOptionAwaitExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkBindAsync_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = RValueFactory.SomeTask(initialValue);

            var numberAfterTry = await numberResult.RValueBindTrySomeAwait(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkBindAsync_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.RValueBindTrySomeAwait(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkBindAsync_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = RValueFactory.SomeTask(initialNumber);

            var rValue = await numberResult.RValueBindTrySomeAwait(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkBindAsync_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.RValueBindTrySomeAwait(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkBindAsyncFunc_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = RValueFactory.SomeTask(initialValue);

            var numberAfterTry = await numberResult.RValueBindTrySomeAwait(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkBindAsyncFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.RValueBindTrySomeAwait(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkBindAsyncFunc_OkResult_ExceptionTry()
        {
            const int initialNumber = 0;
            var numberResult = RValueFactory.SomeTask(initialNumber);

            var rValue = await numberResult.RValueBindTrySomeAwait(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task RValueBindTryOkBindAsyncFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = RValueFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numberResult.RValueBindTrySomeAwait(
                numbers => RValueFactory.SomeTask(Division(numbers)), Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}