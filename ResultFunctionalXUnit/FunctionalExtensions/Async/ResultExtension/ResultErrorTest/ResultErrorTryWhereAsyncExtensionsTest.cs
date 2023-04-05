using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Sync.RExtension.Units.ResultErrorTryExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений. Тесты
    /// </summary>
    public class ResultErrorTryWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ
        /// </summary>
        [Fact]
        public async Task ResultErrorTryWhereAsync_Ok()
        {
            int initialValue = Numbers.Number;
            var numberResult = new ResultError();

            var resultError = await numberResult.ResultErrorTryOkAsync(() => AsyncFunctions.DivisionAsync(initialValue), 
                                                                       Exceptions.ExceptionError());

            Assert.True(resultError.OkStatus);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultErrorTryWhereAsync_Exception()
        {
            const int initialValue = 0;
            var numberResult = new ResultError();

            var resultError = await numberResult.ResultErrorTryOkAsync(() => AsyncFunctions.DivisionAsync(initialValue),
                                                                       Exceptions.ExceptionError());

            Assert.True(resultError.HasErrors);
            Assert.NotNull(resultError.Errors.First().Exception);
        }
    }
}