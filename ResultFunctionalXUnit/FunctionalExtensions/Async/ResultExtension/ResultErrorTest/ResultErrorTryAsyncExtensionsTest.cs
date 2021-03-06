using System.Linq;
using System.Threading.Tasks;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors.ResultErrorTryAsyncExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultErrorTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ
        /// </summary>
        [Fact]
        public async Task ResultValueTry_Ok()
        {
            int initialValue = Numbers.Number;

            var resultValue = await ResultErrorTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueTry_Exception()
        {
            const int initialValue = 0;

            var resultValue = await ResultErrorTryAsync(() => AsyncFunctions.DivisionAsync(initialValue), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }
    }
}