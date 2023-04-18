using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists.RListTryAsyncExtensions;
using static ResultFunctionalXUnit.Mocks.Implementation.AsyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class RListTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task RListTryAsync_Ok_IEnumerable()
        {
            var RValue = await RListTryAsync(() => DivisionCollectionAsync(1), Exceptions.ExceptionError());

            Assert.True(RValue.Success);
            Assert.Equal(await DivisionCollectionAsync(1), RValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task RListTryAsync_Exception_IEnumerable()
        {
            var RValue = await RListTryAsync(() => DivisionCollectionAsync(0), Exceptions.ExceptionError());

            Assert.True(RValue.Failure);
            Assert.NotNull(RValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task RListTryAsyncFunc_Ok_IEnumerable()
        {
            var RValue = await RListTryAsync(() => DivisionCollectionAsync(1), Exceptions.ExceptionFunc());

            Assert.True(RValue.Success);
            Assert.Equal(await DivisionCollectionAsync(1), RValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task RListTryAsyncFunc_Exception_IEnumerable()
        {
            var RValue = await RListTryAsync(() => DivisionCollectionAsync(0), Exceptions.ExceptionFunc());

            Assert.True(RValue.Failure);
            Assert.NotNull(RValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task RListTryAsync_Ok_IReadonlyCollection()
        {
            var RValue = await RListTryAsync(() => DivisionCollectionAsync(1), Exceptions.ExceptionError());

            Assert.True(RValue.Success);
            Assert.Equal(await DivisionCollectionAsync(1), RValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task RListTryAsync_Exception_IReadonlyCollection()
        {
            var RValue = await RListTryAsync(() => DivisionCollectionAsync(0), Exceptions.ExceptionError());

            Assert.True(RValue.Failure);
            Assert.NotNull(RValue.GetErrors().First().Exception);
        }
    }
}