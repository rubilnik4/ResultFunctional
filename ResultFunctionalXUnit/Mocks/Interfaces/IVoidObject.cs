using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResultFunctionalXUnit.Mocks.Interfaces
{
    /// <summary>
    /// Тестовый класс для проверки действий
    /// </summary>
    public interface IVoidObject
    {
        /// <summary>
        /// Тестовый метод
        /// </summary>
        void TestVoid();

        /// <summary>
        /// Тестовый метод
        /// </summary>
        Task TestVoidAsync();

        /// <summary>
        /// Тестовый метод с числовым параметром
        /// </summary>
        void TestNumberVoid(int number);

        /// <summary>
        /// Тестовый метод с числовым параметром коллекцией
        /// </summary>
        void TestNumbersVoid(IEnumerable<int> number);

        /// <summary>
        /// Тестовый асинхронный метод с числовым параметром
        /// </summary>
        Task TestNumberVoidAsync(int number);

        /// <summary>
        /// Тестовый асинхронный метод с числовым параметром коллекцией
        /// </summary>
        Task TestNumbersVoidAsync(IEnumerable<int> number);
    }
}