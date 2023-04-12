using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Преобразование результирующего ответа в значение. Тесты
    /// </summary>
    public class ResultValueToValueBindAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public async Task ResultValueToValueOkBadBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValueTask = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await resultValueTask.ResultValueToValueOkBadBindAsync(
                okFunc: AsyncFunctions.IntToStringAsync,
                badFunc: errors => AsyncFunctions.IntToStringAsync(errors.Count));

            Assert.Equal(initialValue.ToString(), resultAfterWhere);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task ResultValueToValueOkBadBindAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = ErrorData.CreateErrorListTwoTest();
            var resultValueTask = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultValueTask.ResultValueToValueOkBadBindAsync(
                okFunc: AsyncFunctions.IntToStringAsync,
                badFunc: errors => AsyncFunctions.IntToStringAsync(errors.Count));

            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere);
        }
    }
}