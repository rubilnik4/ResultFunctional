using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ResultFunctionalXUnit.Mocks.Implementation
{
    /// <summary>
    /// Тестовые синхронные функции
    /// </summary>
    public static class SyncFunctions
    {
        /// <summary>
        /// Функция деления на число
        /// </summary>
        public static int Division(int divider) => 10 / divider;

        /// <summary>
        /// Функция деления на число
        /// </summary>
        public static void DivisionAction(int _)
        { }

        /// <summary>
        /// Функция деления на коллекцию чисел
        /// </summary>
        public static IEnumerable<int> DivisionByCollection(IEnumerable<int> dividers) =>
            dividers.Select(divider => 10 / divider);

        /// <summary>
        /// Функция деления на коллекцию чисел
        /// </summary>
        public static IEnumerable<int> DivisionCollectionByZero(IEnumerable<int> _) =>
            throw new DivideByZeroException();

        /// <summary>
        /// Функция деления коллекции на число
        /// </summary>
        public static IReadOnlyCollection<int> DivisionCollection(int divider) =>
            new List<int> { 10, 20, 30 }.
            Select(number => number / divider).
            ToList().AsReadOnly();

        /// <summary>
        /// Преобразовать число в коллекцию повторений
        /// </summary>
        public static IEnumerable<int> NumberToCollection(int number) =>
            Enumerable.Repeat(number, 3);
    }
}