using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Преобразование результирующего ответа в значение. Тесты
    /// </summary>
    public class RValueLiftTaskExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public async Task RValueToValueOkBadTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValueTask = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValueTask.RValueLiftMatchTaskAsync(
                number => number.ToString(),
                errors => errors.Count.ToString());

            Assert.Equal(initialValue.ToString(), resultAfterWhere);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task RValueToValueOkBadTaskAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = ErrorData.CreateErrorListTwoTest();
            var rValueTask = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rValueTask.RValueLiftMatchTaskAsync(
                number => number.ToString(),
                errors => errors.Count.ToString());

            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere);
        }
    }
}