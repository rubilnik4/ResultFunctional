using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RMaybeTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений. Тесты
    /// </summary>
    public class RMaybeTryWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ
        /// </summary>
        [Fact]
        public async Task RMaybeTryWhereTaskAsync_Ok()
        {
            int initialValue = Numbers.Number;
            var numberResult = RUnitFactory.SomeTask();

            var RMaybe = await numberResult.ToRMaybeTask().RMaybeTrySomeTask(() => SyncFunctions.Division(initialValue),
                                                                                  Exceptions.ExceptionError());

            Assert.True(RMaybe.Success);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task RMaybeTryWhereTaskAsync_Exception()
        {
            const int initialValue = 0;
            var numberResult = RUnitFactory.SomeTask();

            var RMaybe = await numberResult.ToRMaybeTask().RMaybeTrySomeTask(() => SyncFunctions.Division(initialValue),
                                                                                  Exceptions.ExceptionError());

            Assert.True(RMaybe.Failure);
            Assert.NotNull(RMaybe.GetErrors().First().Exception);
        }
    }
}