using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующей коллекции со значением. Тесты
    /// </summary>
    public class ResultCollectionExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ConcatResultCollection_Ok()
        {
            var resultCollectionFirst = GetRangeNumber().ToResultCollection();
            var resultCollectionSecond = GetRangeNumber().ToResultCollection();
            var results = Enumerable.Empty<IResultCollection<int>>().Append(resultCollectionFirst).Append(resultCollectionSecond);

            var resultCollection = results.ConcatResultCollection();
            var numberRange = resultCollectionFirst.Value.Concat(resultCollectionSecond.Value).ToList();

            Assert.True(resultCollection.OkStatus);
            Assert.True(numberRange.SequenceEqual(resultCollection.Value));
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ConcatResultCollection_Error()
        {
            var resultCollectionFirst = GetRangeNumber().ToResultCollection();
            var resultCollectionErrorFirst = CreateErrorTest().ToResultCollection<int>();
            var resultCollectionErrorSecond = CreateErrorTest().ToResultCollection<int>();
            var results = Enumerable.Empty<IResultCollection<int>>().Append(resultCollectionFirst).Append(resultCollectionErrorFirst).Append(resultCollectionErrorSecond);

            var resultCollection = results.ConcatResultCollection();

            Assert.True(resultCollection.HasErrors);
            Assert.Equal(2, resultCollection.Errors.Count);
        }
    }
}