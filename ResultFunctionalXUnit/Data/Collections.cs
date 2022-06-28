using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctionalXUnit.Data
{
    /// <summary>
    /// Тестовые примеры коллекций
    /// </summary>
    public static class Collections
    {
        /// <summary>
        /// Список чисел
        /// </summary>
        public static IReadOnlyCollection<int> GetRangeNumber() =>
            Enumerable.Range(1, 3).ToList().AsReadOnly();

        /// <summary>
        /// Список чисел с нулем
        /// </summary>
        public static IReadOnlyCollection<int> GetRangeNumberWithZero() =>
            Enumerable.Range(0, 3).ToList().AsReadOnly();

        /// <summary>
        /// Преобразовать коллекцию чисел в коллекцию строк
        /// </summary>
        public static IEnumerable<string> CollectionToString(IEnumerable<int> numbers) =>
            numbers.Select(number => number.ToString());

        /// <summary>
        /// Преобразовать коллекцию чисел в коллекцию строк асинхронно
        /// </summary>
        public static async Task<IEnumerable<string>> CollectionToStringAsync(IEnumerable<int> numbers) =>
            await Task.FromResult(numbers.Select(number => number.ToString()));

        /// <summary>
        /// Преобразовать список чисел в строку
        /// </summary>
        public static string AggregateToString(IEnumerable<int> numbers) =>
            numbers.Aggregate(String.Empty, (previous, next) => previous.ToString() + next);

        /// <summary>
        /// Преобразовать список чисел в строку
        /// </summary>
        public static async Task<string> AggregateToStringAsync(IEnumerable<int> numbers) =>
           await Task.FromResult(numbers.Aggregate(String.Empty, (previous, next) => previous.ToString() + next));

        /// <summary>
        /// Получить количество ошибок списком
        /// </summary>
        public static IList<int> GetListByErrorsCount(IReadOnlyCollection<IErrorResult> errors) =>
            new List<int> { errors.Count };

        /// <summary>
        /// Получить количество ошибок списком
        /// </summary>
        public static IList<string> GetListByErrorsCountString(IReadOnlyCollection<IErrorResult> errors) =>
            new List<string> { errors.Count.ToString() };

        /// <summary>
        /// Получить список с пустой строкой
        /// </summary>
        public static IList<string> GetEmptyStringList() =>
            new List<string> { String.Empty };
    }
}