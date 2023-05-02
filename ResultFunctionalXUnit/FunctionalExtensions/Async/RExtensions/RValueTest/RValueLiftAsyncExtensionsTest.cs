using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Преобразование результирующего ответа в значение. Тесты
    /// </summary>
    public class RValueLiftAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public async Task RValueToValueOkBadAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = await rValue.RValueLiftMatchAsync(
                AsyncFunctions.IntToStringAsync,
                errors => AsyncFunctions.IntToStringAsync(errors.Count));

            Assert.Equal(initialValue.ToString(), resultAfterWhere);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task RValueToValueOkBadAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = ErrorData.CreateErrorListTwoTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = await rValue.RValueLiftMatchAsync(
                AsyncFunctions.IntToStringAsync,
                errors => AsyncFunctions.IntToStringAsync(errors.Count));

            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere);
        }
    }
}