using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для преобразования типов для объекта-задачи. Тесты
    /// </summary>
    public class MapTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка преобразования типа-задачи с помощью функции. Из числа в строку
        /// </summary>
        [Fact]
        public async Task MapTaskAsync_IntToString()
        {
            const int numberInitial = 2;
            var numberTask = Task.FromResult(numberInitial);

            string stringFromNumber = await numberTask.MapTaskAsync(number => number.ToString());

            Assert.Equal(numberInitial.ToString(), stringFromNumber);
        }

        /// <summary>
        /// Проверка преобразования типов-задачи 
        /// </summary>
        [Fact]
        public async Task MapValueToTask()
        {
            const int numberInitial = 2;
            var numberTask = new ValueTask<int>(numberInitial);

            int number = await numberTask.MapValueToTask();

            Assert.Equal(numberInitial, number);
        }
    }
}