using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Extensions.TaskExtensions;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Преобразование результирующего ответа в коллекцию. Тесты
    /// </summary>
    public class ResultCollectionToCollectionAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public async Task ResultCollectionToCollectionAsync_Ok_ReturnNewValue()
        {
            var initialCollection = Collections.GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = await resultCollection.RListLiftMatchAsync(
                okFunc: Collections.CollectionToStringAsync,
                badFunc: _ => Collections.GetEmptyStringList().ToTask());

            Assert.True(Collections.CollectionToString(initialCollection).SequenceEqual(resultAfterWhere));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task ResultCollectionToCollectionAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = ErrorData.CreateErrorListTwoTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = await resultCollection.RListLiftMatchAsync(
                okFunc: Collections.CollectionToStringAsync,
                badFunc: errors => TaskEnumerableExtensions.ToTaskCollection(new List<string> { errors.Count.ToString() }));

            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.First());
        }
    }
}