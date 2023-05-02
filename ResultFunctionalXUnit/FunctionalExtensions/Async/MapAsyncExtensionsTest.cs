using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для асинхронного преобразования типов для объекта-задачи. Тесты
    /// </summary>
    public class MapAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка преобразования типов с помощью функции. Из числа в строку
        /// </summary>
        [Fact]
        public async Task MapBindAsync_IntToString()
        {
            const int numberInitial = 2;
            var numberTask = Task.FromResult(numberInitial);

            string stringFromNumber = await numberTask.MapAwait(AsyncFunctions.IntToStringAsync);

            Assert.Equal(numberInitial.ToString(), stringFromNumber);
        }
    }
}