using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.RExtensions.Values.RValueTryAsyncExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
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
            var resultValue = await RValueTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionError());

            Assert.True(resultValue.Success);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), resultValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueTryAsync_Exception()
        {
            const int initialValue = 0;
            var resultValue = await RValueTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionError());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultValueTryAsyncFunc_Ok()
        {
            int initialValue = Numbers.Number;
            var resultValue = await RValueTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionFunc());

            Assert.True(resultValue.Success);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), resultValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueTryAsyncFunc_Exception()
        {
            const int initialValue = 0;
            var resultValue = await RValueTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionFunc());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }
    }
}