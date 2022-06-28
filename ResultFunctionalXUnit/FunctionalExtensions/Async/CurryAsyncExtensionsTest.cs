using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Sync;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для функций высшего порядка. Тесты
    /// </summary>
    public class CurryAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка преобразования функции высшего порядка для одного аргумента
        /// </summary>
        [Fact]
        public async Task CurryAsync_OneArgumentFunc()
        {
            Func<int, int> plusTwoFunc = number => number + 2;

            var totalFunc = await plusTwoFunc.CurryAsync(Task.FromResult(3));

            Assert.Equal(5, totalFunc.Invoke());
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для двух аргументов
        /// </summary>
        [Fact]
        public async Task CurryAsync_TwoArgumentFunc()
        {
            Func<int, int, int> plusTwoFunc = (numberFirst, numberSecond) => numberFirst + numberSecond;

            var totalFunc = await plusTwoFunc.CurryAsync(Task.FromResult(3));

            Assert.Equal(5, totalFunc.Invoke(2));
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для трех аргументов
        /// </summary>
        [Fact]
        public async Task CurryAsync_ThreeArgumentFunc()
        {
            Func<int, int, int, int> plusThreeFunc =
                (numberFirst, numberSecond, thirdNumber) => numberFirst + numberSecond + thirdNumber;

            var totalFunc = await plusThreeFunc.CurryAsync(Task.FromResult(3));

            Assert.Equal(6, totalFunc.Invoke(2, 1));
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для четырех аргументов
        /// </summary>
        [Fact]
        public async Task CurryAsync_FourArgumentFunc()
        {
            Func<int, int, int, int, int> plusFourFunc =
                (numberFirst, numberSecond, thirdNumber, fourthNumber) => numberFirst + numberSecond + thirdNumber + fourthNumber;

            var totalFunc = await plusFourFunc.CurryAsync(Task.FromResult(4));

            Assert.Equal(8, totalFunc.Invoke(2, 1, 1));
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для пяти аргументов
        /// </summary>
        [Fact]
        public async Task CurryAsync_FiveArgumentFunc()
        {
            Func<int, int, int, int, int, int> plusFiveFunc =
                (numberFirst, numberSecond, thirdNumber, fourthNumber, fiveNumber) =>
                    numberFirst + numberSecond + thirdNumber + fourthNumber + fiveNumber;

            var totalFunc = await plusFiveFunc.CurryAsync(Task.FromResult(6));

            Assert.Equal(12, totalFunc.Invoke(2, 2, 1, 1));
        }
    }
}