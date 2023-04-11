using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.RExtension.Lists.ResultCollectionTryAsyncExtensions;
using static ResultFunctionalXUnit.Mocks.Implementation.AsyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultCollectionTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsync_Ok_IEnumerable()
        {
            var resultValue = await ResultCollectionTryAsync(() => DivisionCollectionAsync(1), Exceptions.ExceptionError());

            Assert.True(resultValue.Success);
            Assert.Equal(await DivisionCollectionAsync(1), resultValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsync_Exception_IEnumerable()
        {
            var resultValue = await ResultCollectionTryAsync(() => DivisionCollectionAsync(0), Exceptions.ExceptionError());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncFunc_Ok_IEnumerable()
        {
            var resultValue = await ResultCollectionTryAsync(() => DivisionCollectionAsync(1), Exceptions.ExceptionFunc());

            Assert.True(resultValue.Success);
            Assert.Equal(await DivisionCollectionAsync(1), resultValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsyncFunc_Exception_IEnumerable()
        {
            var resultValue = await ResultCollectionTryAsync(() => DivisionCollectionAsync(0), Exceptions.ExceptionFunc());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsync_Ok_IReadonlyCollection()
        {
            var resultValue = await ResultCollectionTryAsync(() => DivisionCollectionAsync(1), Exceptions.ExceptionError());

            Assert.True(resultValue.Success);
            Assert.Equal(await DivisionCollectionAsync(1), resultValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryAsync_Exception_IReadonlyCollection()
        {
            var resultValue = await ResultCollectionTryAsync(() => DivisionCollectionAsync(0), Exceptions.ExceptionError());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }
    }
}