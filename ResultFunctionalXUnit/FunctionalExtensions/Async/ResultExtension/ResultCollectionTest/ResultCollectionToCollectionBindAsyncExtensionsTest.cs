using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Extensions.TaskExtensions;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Преобразование результирующего ответа в коллекцию. Тесты
    /// </summary>
    public class ResultCollectionToCollectionBindAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public async Task ResultCollectionToCollectionAsync_Ok_ReturnNewValue()
        {
            var initialCollection = Collections.GetRangeNumber();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollection(initialCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionToCollectionOkBadBindAsync(
                okFunc: Collections.CollectionToStringAsync,
                badFunc: _ => TaskEnumerableExtensions.ToTaskEnumerable(Collections.GetEmptyStringList()));

            Assert.True(Collections.CollectionToString(initialCollection).SequenceEqual(resultAfterWhere));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task ResultCollectionToCollectionAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = ErrorData.CreateErrorListTwoTest();
            var resultCollection = ResultCollectionFactory.CreateTaskResultCollectionError<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionToCollectionOkBadBindAsync(
                okFunc: Collections.CollectionToStringAsync,
                badFunc: errors => TaskEnumerableExtensions.ToTaskEnumerable(new List<string> { errors.Count.ToString() }));

            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.First());
        }
    }
}