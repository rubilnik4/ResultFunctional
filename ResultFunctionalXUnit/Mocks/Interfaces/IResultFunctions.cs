using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Options;

namespace ResultFunctionalXUnit.Mocks.Interfaces
{
    /// <summary>
    /// Тестовые функции для результирующих ответов
    /// </summary>
    public interface IResultFunctions
    {
        /// <summary>
        /// Преобразовать число в результирующий ответ 
        /// </summary>
        IROption NumberToResult(int number);

        /// <summary>
        /// Преобразовать число в результирующий ответ асинхронно
        /// </summary>
        Task<IROption> NumberToResultAsync(int number);

        /// <summary>
        /// Преобразовать коллекцию чисел в результирующий ответ 
        /// </summary>
        IROption NumbersToResult(IReadOnlyCollection<int> numbers);

        /// <summary>
        /// Преобразовать коллекцию чисел в результирующий ответ асинхронно
        /// </summary>
        Task<IROption> NumbersToResultAsync(IReadOnlyCollection<int> numbers);
    }
}