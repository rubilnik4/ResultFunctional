
using ResultFunctional.FunctionalExtensions.Sync;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync
{
    /// <summary>
    /// Методы расширения для преобразования типов. Тесты
    /// </summary>
    public class MapExtensionsTest
    {
        /// <summary>
        /// Проверка преобразование типов с помощью функции. Из числа в строку
        /// </summary>
        [Fact]
        public void Map_IntToString()
        {
            const int number = 2;

            string stringFromNumber = number.Map(numberConverting => numberConverting.ToString());

            Assert.Equal(number.ToString(), stringFromNumber);
        }
    }
}