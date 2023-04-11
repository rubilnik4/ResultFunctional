using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using ResultFunctionalXUnit.Data;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Преобразование результирующего ответа в коллекцию. Тесты
    /// </summary>
    public class ResultCollectionToCollectionExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionToCollection_Ok_ReturnNewValue()
        {
            var initialCollection = Collections.GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = resultCollection.ResultCollectionToCollectionOkBad(
                okFunc: Collections.CollectionToString,
                badFunc: _ => Collections.GetEmptyStringList());

            Assert.True(Collections.CollectionToString(initialCollection).SequenceEqual(resultAfterWhere));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public void ResultCollectionToCollection_Bad_ReturnNewValue()
        {
            var errorsInitial = ErrorData.CreateErrorListTwoTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionToCollectionOkBad(
                okFunc: Collections.CollectionToString,
                badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.First());
        }
    }
}