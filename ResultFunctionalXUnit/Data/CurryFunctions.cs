using System;
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
        /// Сложить два числа и преобразовать в строку
        /// </summary>
        public static Func<int, int, string> AggregateTwoToString =>
            (first, second) => (first + second).ToString();

        /// <summary>
        /// Сложить три числа и преобразовать в строку
        /// </summary>
        public static Func<int, int, int, string> AggregateThreeToString =>
            (first, second, third) => (first + second + third).ToString();

        /// <summary>
        /// Сложить четыре числа и преобразовать в строку
        /// </summary>
        public static Func<int, int, int, int, string> AggregateFourToString =>
            (first, second, third, fourth) => (first + second + third + fourth).ToString();

        /// <summary>
        /// Сложить пять чисел и преобразовать в строку
        /// </summary>
        public static Func<int, int, int, int, int, string> AggregateFiveToString =>
            (first, second, third, fourth, fifth) => (first + second + third + fourth + fifth).ToString();

        /// <summary>
        /// Преобразовать число в строку асинхронно
        /// </summary>
        public static Func<int, Task<string>> IntToStringAsync =>
            number => Task.FromResult(number.ToString());

        /// <summary>
        /// Сложить два числа и преобразовать в строку
        /// </summary>
        public static Func<int, int, Task<string>> AggregateTwoToStringAsync =>
            (first, second) => Task.FromResult((first + second).ToString());

        /// <summary>
        /// Сложить три числа и преобразовать в строку
        /// </summary>
        public static Func<int, int, int, Task<string>> AggregateThreeToStringAsync =>
            (first, second, third) =>  Task.FromResult((first + second + third).ToString());

        /// <summary>
        /// Сложить четыре числа и преобразовать в строку
        /// </summary>
        public static Func<int, int, int, int, Task<string>> AggregateFourToStringAsync =>
            (first, second, third, fourth) => Task.FromResult((first + second + third + fourth).ToString());

        /// <summary>
        /// Сложить пять чисел и преобразовать в строку
        /// </summary>
        public static Func<int, int, int, int, int, Task<string>> AggregateFiveToStringAsync => 
            (first, second, third, fourth, fifth) => Task.FromResult((first + second + third + fourth + fifth).ToString());
    }
}