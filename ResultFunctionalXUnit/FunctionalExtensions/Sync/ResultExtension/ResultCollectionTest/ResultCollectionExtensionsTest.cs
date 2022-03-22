using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
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
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultCollectionNullCheck_Ok()
        {
            var collection = new List<string?> { "1", "2", "3" };

            var resultString = collection.ToResultCollectionNullCheck(CreateErrorTest());

            Assert.True(resultString.OkStatus);
            Assert.True(collection.SequenceEqual(resultString.Value));
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultCollectionNullCheck_ErrorCollectionNull()
        {
            List<string?>? collection = null;
            var initialError = CreateErrorTest();

            var resultString = collection.ToResultCollectionNullCheck(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultCollectionNullCheck_ErrorNull()
        {
            var collection = new List<string?> { "1", null, "3" };
            var initialError = CreateErrorTest();

            var resultString = collection.ToResultCollectionNullCheck(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError));
        }


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