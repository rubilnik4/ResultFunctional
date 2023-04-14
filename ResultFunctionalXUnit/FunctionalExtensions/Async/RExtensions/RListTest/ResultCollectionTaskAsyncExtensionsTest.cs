using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.Models.Lists;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ConcatResultCollection_Ok()
        {
            var resultCollectionFirst = GetRangeNumber().ToRList();
            var resultCollectionSecond = GetRangeNumber().ToRList();
            var results = Enumerable.Empty<IRList<int>>().Append(resultCollectionFirst).Append(resultCollectionSecond).
                                     Map(Task.FromResult);

            var resultCollection = await results.RListFoldTask();
            var numberRange = resultCollectionFirst.GetValue().Concat(resultCollectionSecond.GetValue()).ToList();

            Assert.True(resultCollection.Success);
            Assert.True(numberRange.SequenceEqual(resultCollection.GetValue()));
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ConcatResultCollection_Error()
        {
            var resultCollectionFirst = GetRangeNumber().ToRList();
            var resultCollectionErrorFirst = CreateErrorTest().ToRList<int>();
            var resultCollectionErrorSecond = CreateErrorTest().ToRList<int>();
            var results = Enumerable.Empty<IRList<int>>().Append(resultCollectionFirst).Append(resultCollectionErrorFirst).Append(resultCollectionErrorSecond).
                                     Map(Task.FromResult);

            var resultCollection = await results.RListFoldTask();

            Assert.True(resultCollection.Failure);
            Assert.Equal(2, resultCollection.GetErrors().Count);
        }
    }
}