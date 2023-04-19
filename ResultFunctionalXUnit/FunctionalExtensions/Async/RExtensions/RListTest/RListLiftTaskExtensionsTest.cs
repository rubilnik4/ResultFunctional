using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Преобразование результирующего ответа в коллекцию. Тесты
    /// </summary>
    public class RListLiftTaskExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public async Task  RListToCollectionTaskAsync_Ok_ReturnNewValue()
        {
            var initialCollection = Collections.GetRangeNumber();
            var rList = RListFactory.SomeTask(initialCollection);

            var resultAfterWhere = await rList.RListLiftMatchTask(
                Collections.CollectionToString,
                _ => Collections.GetEmptyStringList());

            Assert.True(Collections.CollectionToString(initialCollection).SequenceEqual(resultAfterWhere));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task RListToCollectionTaskAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = ErrorData.CreateErrorListTwoTest();
            var rList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rList.RListLiftMatchTask(
                Collections.CollectionToString,
                errors => new List<string> { errors.Count.ToString() });

            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.First());
        }
    }
}