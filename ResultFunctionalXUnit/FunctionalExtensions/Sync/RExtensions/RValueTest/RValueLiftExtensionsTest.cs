using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Преобразование результирующего ответа в значение. Тесты
    /// </summary>
    public class RValueLiftExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public void RValueToValueOkBad_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = rValue.RValueLiftMatch(
                number => number.ToString(),
                errors => errors.Count.ToString());

            Assert.Equal(initialValue.ToString(), resultAfterWhere);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void RValueToValueOkBad_Bad_ReturnNewValue()
        {
            var errorsInitial = ErrorData.CreateErrorListTwoTest();
            var rValue = errorsInitial.ToRValue<int>();
      
            var resultAfterWhere = rValue.RValueLiftMatch(
                number => number.ToString(),
                errors => errors.Count.ToString());

            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere);
        }
    }
}