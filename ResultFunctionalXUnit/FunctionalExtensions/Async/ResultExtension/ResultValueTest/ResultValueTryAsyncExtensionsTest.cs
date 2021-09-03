using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
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
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultValueTryAsyncFunc_Ok()
        {
            int initialValue = Numbers.Number;
            var resultValue = await ResultValueTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionFunc());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), resultValue.Value);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueTryAsyncFunc_Exception()
        {
            const int initialValue = 0;
            var resultValue = await ResultValueTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionFunc());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }
    }
}