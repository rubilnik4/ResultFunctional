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
    public class CurryBindAsyncExtensionsTest
    {
        /// <summary>
        /// Проверка преобразования функции высшего порядка для одного аргумента
        /// </summary>
        [Fact]
        public async Task CurryBindAsync_OneArgumentFunc()
        {
            Func<int, int> plusTwoFunc = number => number + 2;
            var taskFunc = Task.FromResult(plusTwoFunc);

            var totalFunc = await taskFunc.CurryAwait(Task.FromResult(3));

            Assert.Equal(5, totalFunc);
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для двух аргументов
        /// </summary>
        [Fact]
        public async Task CurryBindAsync_TwoArgumentFunc()
        {
            Func<int, int, int> plusTwoFunc = (numberFirst, numberSecond) => numberFirst + numberSecond;
            var taskFunc = Task.FromResult(plusTwoFunc);

            var totalFunc = await taskFunc.CurryAwait(Task.FromResult(3));

            Assert.Equal(5, totalFunc.Invoke(2));
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для трех аргументов
        /// </summary>
        [Fact]
        public async Task CurryBindAsync_ThreeArgumentFunc()
        {
            Func<int, int, int, int> plusThreeFunc =
                (numberFirst, numberSecond, thirdNumber) => numberFirst + numberSecond + thirdNumber;
            var taskFunc = Task.FromResult(plusThreeFunc);

            var totalFunc = await taskFunc.CurryAwait(Task.FromResult(3));

            Assert.Equal(6, totalFunc.Invoke(2, 1));
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для четырех аргументов
        /// </summary>
        [Fact]
        public async Task CurryBindAsync_FourArgumentFunc()
        {
            Func<int, int, int, int, int> plusFourFunc =
                (numberFirst, numberSecond, thirdNumber, fourthNumber) => numberFirst + numberSecond + thirdNumber + fourthNumber;
            var taskFunc = Task.FromResult(plusFourFunc);

            var totalFunc = await taskFunc.CurryAwait(Task.FromResult(4));

            Assert.Equal(8, totalFunc.Invoke(2, 1, 1));
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для пяти аргументов
        /// </summary>
        [Fact]
        public async Task CurryBindAsync_FiveArgumentFunc()
        {
            Func<int, int, int, int, int, int> plusFiveFunc =
                (numberFirst, numberSecond, thirdNumber, fourthNumber, fiveNumber) =>
                    numberFirst + numberSecond + thirdNumber + fourthNumber + fiveNumber;
            var taskFunc = Task.FromResult(plusFiveFunc);

            var totalFunc = await taskFunc.CurryAwait(Task.FromResult(6));

            Assert.Equal(12, totalFunc.Invoke(2, 2, 1, 1));
        }
    }
}