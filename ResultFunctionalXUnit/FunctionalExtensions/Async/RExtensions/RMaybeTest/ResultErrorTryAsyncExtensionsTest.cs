using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe.RMaybeTryAsyncExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RMaybeTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class RMaybeTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ
        /// </summary>
        [Fact]
        public async Task RValueTry_Ok()
        {
            int initialValue = Numbers.Number;

            var RValue = await RMaybeTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionError());

            Assert.True(RValue.Success);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueTry_Exception()
        {
            const int initialValue = 0;

            var RValue = await RMaybeTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionError());

            Assert.True(RValue.Failure);
            Assert.NotNull(RValue.GetErrors().First().Exception);
        }
    }
}