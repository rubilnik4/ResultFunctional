using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа со значением в обертке. Тесты
    /// </summary>
    public class ResultValueRawWhereExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки в обертке
        /// </summary>   
        [Fact]
        public void ResultValueRawOk_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueRawOk(result => new ResultValue<string>(result.GetValue().ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе с ошибкой в обертке
        /// </summary>   
        [Fact]
        public void ResultValueRawOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere = resultValue.ResultValueRawOk(result => new ResultValue<string>(result.GetValue().ToString()));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе без ошибки в обертке
        /// </summary>   
        [Fact]
        public void ResultValueRawBad_Ok_ReturnInitial()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueRawBad(result => new ResultValue<int>(result.GetValue() * 2));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialValue, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой в обертке
        /// </summary>   
        [Fact]
        public void ResultValueRawBad_Bad_ReturnInitial()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere = resultValue.ResultValueRawBad(result => new ResultValue<int>(result.GetErrors().Count * 2));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count * 2, resultAfterWhere.GetValue());
        }
    }
}