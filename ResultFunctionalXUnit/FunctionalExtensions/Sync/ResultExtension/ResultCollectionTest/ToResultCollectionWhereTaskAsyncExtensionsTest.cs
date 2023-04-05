using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using ResultFunctional.Models.Interfaces.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Преобразование значений в результирующий ответ коллекции с условием. Тесты
    /// </summary>
    public class ToResultCollectionWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public void ToResultCollectionWhere_Ok()
        {
            var initialCollection = GetRangeNumber();

            var result = initialCollection.ToRListWhere(_ => true,
                                                                   _ => CreateErrorTest());

            Assert.True(result.OkStatus);
            Assert.True(initialCollection.SequenceEqual(result.Value));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToResultCollectionWhere_BadError()
        {
            var initialCollection = GetRangeNumber();
            var errorInitial = CreateErrorTest();

            var result = initialCollection.ToRListWhere(_ => false,
                                                                   _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }
    }
}