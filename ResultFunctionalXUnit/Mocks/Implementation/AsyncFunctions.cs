using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Sync;

namespace ResultFunctionalXUnit.Mocks.Implementation
{
    /// <summary>
    /// Примеры асинхронных функций
    /// </summary>
    public static class AsyncFunctions
    {
        /// <summary>
        /// Преобразовать число в строку асинхронно
        /// </summary>
        public static async Task<string> IntToStringAsync(int number) =>
            await Task.FromResult(number.ToString());

        /// <summary>
        /// Функция деления на ноль асинхронно
        /// </summary>
        public static async Task<int> DivisionAsync(int divider) => await Task.FromResult(10 / divider);

        /// <summary>
        /// Функция деления на ноль коллекции асинхронно
        /// </summary>
        public static async Task<List<int>> DivisionListAsync(int divider) =>
            await new List<int> { 10, 20, 30 }.
            Select(number => number / divider).
            ToList().
            Map(Task.FromResult);

        /// <summary>
        /// Функция деления на ноль коллекции асинхронно
        /// </summary>
        public static async Task<IReadOnlyCollection<int>> DivisionCollectionAsync(int divider) =>
            await DivisionListAsync(divider).
            MapTaskAsync(collection => collection.AsReadOnly());

        /// <summary>
        /// Функция деления на ноль коллекции асинхронно
        /// </summary>
        public static async Task<IEnumerable<int>> DivisionEnumerableAsync(int divider) =>
            await DivisionListAsync(divider).
            MapTaskAsync(collection => collection.AsReadOnly());

        /// <summary>
        /// Функция деления на коллекцию чисел асинхронно
        /// </summary>
        public static async Task<IReadOnlyCollection<int>> DivisionByCollectionAsync(IEnumerable<int> dividers) =>
            await Task.FromResult(dividers.Select(divider => 10 / divider).ToList());

        /// <summary>
        /// Функция деления на коллекцию чисел
        /// </summary>
        public static Task<IReadOnlyCollection<int>> DivisionCollectionByZeroAsync(IEnumerable<int> dividers) =>
            throw new DivideByZeroException(dividers.ToString());

        /// <summary>
        /// Преобразовать число в коллекцию повторений
        /// </summary>
        public static async Task<IReadOnlyCollection<int>> NumberToCollectionAsync(int number) =>
            await Task.FromResult(Enumerable.Repeat(number, 3).ToList());
    }
}