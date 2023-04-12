using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
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
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = resultValue.RValueBindListSome(
                number => NumberToCollection(number).ToRList());

            Assert.True(resultAfterWhere.Success);
            Assert.True(NumberToCollection(initialValue).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе со связыванием с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueBindOkToCollection_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = resultValue.RValueBindListSome(
                number => NumberToCollection(number).ToRList());

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }
    }
}