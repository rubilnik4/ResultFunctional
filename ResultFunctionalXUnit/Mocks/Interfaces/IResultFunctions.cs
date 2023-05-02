using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Maybe;

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
        IRMaybe NumberToResult(int number);

        /// <summary>
        /// Преобразовать число в результирующий ответ асинхронно
        /// </summary>
        Task<IRMaybe> NumberToResultAsync(int number);

        /// <summary>
        /// Преобразовать коллекцию чисел в результирующий ответ 
        /// </summary>
        IRMaybe NumbersToResult(IReadOnlyCollection<int> numbers);

        /// <summary>
        /// Преобразовать коллекцию чисел в результирующий ответ асинхронно
        /// </summary>
        Task<IRMaybe> NumbersToResultAsync(IReadOnlyCollection<int> numbers);
    }
}