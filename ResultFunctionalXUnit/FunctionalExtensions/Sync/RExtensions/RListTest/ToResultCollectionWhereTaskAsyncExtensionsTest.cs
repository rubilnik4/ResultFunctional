using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest
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

            Assert.True(result.Success);
            Assert.True(initialCollection.SequenceEqual(result.GetValue()));
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

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }
    }
}