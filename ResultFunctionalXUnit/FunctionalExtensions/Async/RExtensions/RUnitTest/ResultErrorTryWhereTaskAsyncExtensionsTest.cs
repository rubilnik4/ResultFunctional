using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Units;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RUnitTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений. Тесты
    /// </summary>
    public class ResultErrorTryWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ
        /// </summary>
        [Fact]
        public async Task ResultErrorTryWhereTaskAsync_Ok()
        {
            int initialValue = Numbers.Number;
            var numberResult = RUnitFactory.SomeTask();

            var resultError = await numberResult.ResultErrorTryOkTaskAsync(() => SyncFunctions.Division(initialValue), 
                                                                       Exceptions.ExceptionError());

            Assert.True(resultError.Success);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultErrorTryWhereTaskAsync_Exception()
        {
            const int initialValue = 0;
            var numberResult = RUnitFactory.SomeTask();

            var resultError = await numberResult.ResultErrorTryOkTaskAsync(() => SyncFunctions.Division(initialValue),
                                                                       Exceptions.ExceptionError());

            Assert.True(resultError.Failure);
            Assert.NotNull(resultError.GetErrors().First().Exception);
        }
    }
}