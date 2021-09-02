using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа со связыванием с значением с возвращением к коллекции. Тесты
    /// </summary>
    public class ResultValueBindWhereToCollectionExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного условия в результирующем ответе со связыванием без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueBindOkToCollection_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueBindOkToCollection(
                number => new ResultCollection<int>(NumberToCollection(number)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(NumberToCollection(initialValue).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе со связыванием с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueBindOkToCollection_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere = resultValue.ResultValueBindOkToCollection(
                number => new ResultCollection<int>(NumberToCollection(number)));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }
    }
}