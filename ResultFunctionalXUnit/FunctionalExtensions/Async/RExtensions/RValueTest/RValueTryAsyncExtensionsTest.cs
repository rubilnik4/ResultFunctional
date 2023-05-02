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
    public class RValueTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task RValueTryAsync_Ok()
        {
            int initialValue = Numbers.Number;
            var rValue = await RValueTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionError());

            Assert.True(rValue.Success);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), rValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueTryAsync_Exception()
        {
            const int initialValue = 0;
            var rValue = await RValueTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionError());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task RValueTryAsyncFunc_Ok()
        {
            int initialValue = Numbers.Number;
            var rValue = await RValueTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionFunc());

            Assert.True(rValue.Success);
            Assert.Equal(await AsyncFunctions.DivisionAsync(initialValue), rValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueTryAsyncFunc_Exception()
        {
            const int initialValue = 0;
            var rValue = await RValueTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionFunc());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }
    }
}