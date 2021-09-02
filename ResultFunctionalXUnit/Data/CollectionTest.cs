using System.Collections.Generic;
using Xunit;

namespace ResultFunctionalXUnit.Data
{
    /// <summary>
    /// Тесты на тестовые данные (дожили)
    /// </summary>
    public class CollectionTest
    {
        /// <summary>
        /// Преобразовать список чисел в строку. Верно
        /// </summary>
        [Fact]
        public void AggregateToString_Ok()
        {
            var numberCollection = new List<int> { 1, 2, 3 };

            string collectionInString = Collections.AggregateToString(numberCollection);

            Assert.Equal("123", collectionInString);
        }
    }
}