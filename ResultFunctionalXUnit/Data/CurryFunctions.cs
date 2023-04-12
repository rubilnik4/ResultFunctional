using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultFunctionalXUnit.Data
{
    /// <summary>
    /// Примеры функций каррирования
    /// </summary>
    public static class CurryFunctions
    {
        /// <summary>
        /// Преобразовать число в строку
        /// </summary>
        public static Func<int, string> IntToString =>
            number => number.ToString();

        /// <summary>
        /// Преобразовать коллекцию чисел в строку
        /// </summary>
        public static Func<IReadOnlyCollection<int>, string> IntCollectionToString =>
            numbers => numbers.Aggregate(String.Empty, (first, second) => first + second);

        /// <summary>
        /// Сложить два числа и преобразовать в строку
        /// </summary>
        public static Func<int, int, string> AggregateTwoToString =>
            (first, second) => (first + second).ToString();

        /// <summary>
        /// Сложить коллекцию чисел и преобразовать в строку
        /// </summary>
        public static Func<IReadOnlyCollection<int>, int, string> AggregateCollectionTwoToString =>
            (first, second) => (first.Sum() + second).ToString();

        /// <summary>
        /// Сложить три числа и преобразовать в строку
        /// </summary>
        public static Func<int, int, int, string> AggregateThreeToString =>
            (first, second, third) => (first + second + third).ToString();

        /// <summary>
        /// Сложить коллекцию чисел и преобразовать в строку
        /// </summary>
        public static Func<IReadOnlyCollection<int>, int, int, string> AggregateCollectionThreeToString =>
                (first, second, third) => (first.Sum() + second + third).ToString();

        /// <summary>
        /// Сложить четыре числа и преобразовать в строку
        /// </summary>
        public static Func<int, int, int, int, string> AggregateFourToString =>
            (first, second, third, fourth) => (first + second + third + fourth).ToString();

        /// <summary>
        /// Сложить коллекцию чисел и преобразовать в строку
        /// </summary>
        public static Func<IReadOnlyCollection<int>, int, int, int, string> AggregateCollectionFourToString =>
            (first, second, third, fourth) => (first.Sum() + second + third + fourth).ToString();

        /// <summary>
        /// Сложить пять чисел и преобразовать в строку
        /// </summary>
        public static Func<int, int, int, int, int, string> AggregateFiveToString =>
            (first, second, third, fourth, fifth) => (first + second + third + fourth + fifth).ToString();

        /// <summary>
        /// Сложить пять чисел и преобразовать в строку
        /// </summary>
        public static Func<IReadOnlyCollection<int>, int, int, int, int, string> AggregateCollectionFiveToString =>
            (first, second, third, fourth, fifth) => (first.Sum() + second + third + fourth + fifth).ToString();

        /// <summary>
        /// Преобразовать число в строку асинхронно
        /// </summary>
        public static Func<int, Task<string>> IntToStringAsync =>
            number => Task.FromResult(number.ToString());

        /// <summary>
        /// Преобразовать число в строку асинхронно
        /// </summary>
        public static Func<IReadOnlyCollection<int>, Task<string>> IntCollectionToStringAsync =>
            number => Task.FromResult(IntCollectionToString(number));

        /// <summary>
        /// Сложить два числа и преобразовать в строку
        /// </summary>
        public static Func<int, int, Task<string>> AggregateTwoToStringAsync =>
            (first, second) => Task.FromResult(AggregateTwoToString(first, second));

        /// <summary>
        /// Сложить два числа и преобразовать в строку
        /// </summary>
        public static Func<IReadOnlyCollection<int>, int, Task<string>> AggregateCollectionTwoToStringAsync =>
            (first, second) => Task.FromResult(AggregateCollectionTwoToString(first, second));

        /// <summary>
        /// Сложить три числа и преобразовать в строку
        /// </summary>
        public static Func<int, int, int, Task<string>> AggregateThreeToStringAsync =>
            (first, second, third) => Task.FromResult(AggregateThreeToString(first, second, third));

        /// <summary>
        /// Сложить три числа и преобразовать в строку
        /// </summary>
        public static Func<IReadOnlyCollection<int>, int, int, Task<string>> AggregateCollectionThreeToStringAsync =>
            (first, second, third) => Task.FromResult(AggregateCollectionThreeToString(first, second, third));

        /// <summary>
        /// Сложить четыре числа и преобразовать в строку
        /// </summary>
        public static Func<int, int, int, int, Task<string>> AggregateFourToStringAsync =>
            (first, second, third, fourth) => Task.FromResult(AggregateFourToString(first, second, third, fourth));

        /// <summary>
        /// Сложить четыре числа и преобразовать в строку
        /// </summary>
        public static Func<IReadOnlyCollection<int>, int, int, int, Task<string>> AggregateCollectionFourToStringAsync =>
            (first, second, third, fourth) => Task.FromResult(AggregateCollectionFourToString(first, second, third, fourth));

        /// <summary>
        /// Сложить пять чисел и преобразовать в строку
        /// </summary>
        public static Func<int, int, int, int, int, Task<string>> AggregateFiveToStringAsync =>
            (first, second, third, fourth, fifth) => Task.FromResult(AggregateFiveToString(first, second, third, fourth, fifth));

        /// <summary>
        /// Сложить пять чисел и преобразовать в строку
        /// </summary>
        public static Func<IReadOnlyCollection<int>, int, int, int, int, Task<string>> AggregateCollectionFiveToStringAsync =>
            (first, second, third, fourth, fifth) => Task.FromResult(AggregateCollectionFiveToString(first, second, third, fourth, fifth));
    }
}