using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Results;

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
        IResultError NumberToResult(int number);

        /// <summary>
        /// Преобразовать число в результирующий ответ асинхронно
        /// </summary>
        Task<IResultError> NumberToResultAsync(int number);

        /// <summary>
        /// Преобразовать коллекцию чисел в результирующий ответ 
        /// </summary>
        IResultError NumbersToResult(IReadOnlyCollection<int> numbers);

        /// <summary>
        /// Преобразовать коллекцию чисел в результирующий ответ асинхронно
        /// </summary>
        Task<IResultError> NumbersToResultAsync(IReadOnlyCollection<int> numbers);
    }
}