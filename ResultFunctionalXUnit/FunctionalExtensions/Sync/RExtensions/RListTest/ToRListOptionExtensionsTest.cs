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
    public class ToRListOptionExtensionsTest
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public void ToRListWhere_Ok()
        {
            var initialCollection = GetRangeNumber();

            var result = initialCollection.ToRListOption(_ => true,
                                                         _ => CreateErrorTest());

            Assert.True(result.Success);
            Assert.True(initialCollection.SequenceEqual(result.GetValue()));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToRListWhere_BadError()
        {
            var initialCollection = GetRangeNumber();
            var errorInitial = CreateErrorTest();

            var result = initialCollection.ToRListOption(_ => false,
                                                                   _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }
    }
}