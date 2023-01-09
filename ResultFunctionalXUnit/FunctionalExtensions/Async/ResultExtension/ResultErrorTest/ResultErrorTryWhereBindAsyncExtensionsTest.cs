using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors.ResultErrorTryExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений. Тесты
    /// </summary>
    public class ResultErrorTryWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ
        /// </summary>
        [Fact]
        public async Task ResultErrorTryWhereBindAsync_Ok()
        {
            int initialValue = Numbers.Number;
            var numberResult = ResultErrorFactory.CreateTaskResultError();

            var resultError = await numberResult.ResultErrorTryOkBindAsync(() => AsyncFunctions.DivisionAsync(initialValue), 
                                                                       Exceptions.ExceptionError());

            Assert.True(resultError.OkStatus);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultErrorTryWhereBindAsync_Exception()
        {
            const int initialValue = 0;
            var numberResult = ResultErrorFactory.CreateTaskResultError();

            var resultError = await numberResult.ResultErrorTryOkBindAsync(() => AsyncFunctions.DivisionAsync(initialValue),
                                                                       Exceptions.ExceptionError());

            Assert.True(resultError.HasErrors);
            Assert.NotNull(resultError.Errors.First().Exception);
        }
    }
}