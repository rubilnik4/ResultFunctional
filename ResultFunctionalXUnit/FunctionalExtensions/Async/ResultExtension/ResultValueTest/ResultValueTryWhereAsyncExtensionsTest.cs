using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Values;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues.ResultValueTryAsyncExtensions;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultValueTryWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkAsync_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = initialValue.ToRValue();

            var numberAfterTry = await numberResult.ResultValueTryOkAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkAsync_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = await numberResult.ResultValueTryOkAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkAsync_OkResult_ExceptionTry()
        {
            const int initialValue = 0;
            var numberResult = initialValue.ToRValue();

            var resultValue = await numberResult.ResultValueTryOkAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkAsync_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = await numberResult.ResultValueTryOkAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkAsyncFunc_OkResult_OkTry()
        {
            int initialValue = Numbers.Number;
            var numberResult = initialValue.ToRValue();

            var numberAfterTry = await numberResult.ResultValueTryOkAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Success);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), numberAfterTry.GetValue());
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkAsyncFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = await numberResult.ResultValueTryOkAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkAsyncFunc_OkResult_ExceptionTry()
        {
            const int initialValue = 0;
            var numberResult = initialValue.ToRValue();

            var resultValue = await numberResult.ResultValueTryOkAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение
        /// </summary>
        [Fact]
        public async Task ResultValueTryOkAsyncFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numberResult = initialError.ToRValue<int>();

            var numberAfterTry = await numberResult.ResultValueTryOkAsync(AsyncFunctions.DivisionAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}