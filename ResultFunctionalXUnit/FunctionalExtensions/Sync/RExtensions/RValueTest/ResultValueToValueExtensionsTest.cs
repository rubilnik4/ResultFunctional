using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Преобразование результирующего ответа в значение. Тесты
    /// </summary>
    public class ResultValueToValueExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public void ResultValueToValueOkBad_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = resultValue.ResultValueToValueOkBad(
                okFunc: number => number.ToString(),
                badFunc: errors => errors.Count.ToString());

            Assert.Equal(initialValue.ToString(), resultAfterWhere);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultValueToValueOkBad_Bad_ReturnNewValue()
        {
            var errorsInitial = ErrorData.CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRValue<int>();
      
            var resultAfterWhere = resultValue.ResultValueToValueOkBad(
                okFunc: number => number.ToString(),
                badFunc: errors => errors.Count.ToString());

            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere);
        }
    }
}