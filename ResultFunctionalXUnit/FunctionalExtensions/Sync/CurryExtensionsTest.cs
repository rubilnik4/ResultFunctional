using System;
using ResultFunctional.FunctionalExtensions.Sync;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync
{
    /// <summary>
    /// Методы расширения для функций высшего порядка. Тесты
    /// </summary>
    public class CurryExtensionsTest
    {
        /// <summary>
        /// Проверка преобразования функции высшего порядка для одного аргумента
        /// </summary>
        [Fact]
        public void Curry_OneArgumentFunc()
        {
            Func<int, int> plusTwoFunc = number => number + 2;

            var totalFunc = plusTwoFunc.Curry(3);

            Assert.Equal(5, totalFunc.Invoke());
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для двух аргументов
        /// </summary>
        [Fact]
        public void Curry_TwoArgumentFunc()
        {
            Func<int, int, int> plusTwoFunc = (numberFirst, numberSecond) => numberFirst + numberSecond;

            var totalFunc = plusTwoFunc.Curry(3);

            Assert.Equal(5, totalFunc.Invoke(2));
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для трех аргументов
        /// </summary>
        [Fact]
        public void Curry_ThreeArgumentFunc()
        {
            Func<int, int, int, int> plusThreeFunc = 
                (numberFirst, numberSecond, thirdNumber) => numberFirst + numberSecond + thirdNumber;

            var totalFunc = plusThreeFunc.Curry(3);

            Assert.Equal(6, totalFunc.Invoke(2, 1));
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для четырех аргументов
        /// </summary>
        [Fact]
        public void Curry_FourArgumentFunc()
        {
            Func<int, int, int, int, int> plusFourFunc =
                (numberFirst, numberSecond, thirdNumber, fourthNumber) => numberFirst + numberSecond + thirdNumber + fourthNumber;

            var totalFunc = plusFourFunc.Curry(4);

            Assert.Equal(8, totalFunc.Invoke(2, 1, 1));
        }

        /// <summary>
        /// Проверка преобразования функции высшего порядка для пяти аргументов
        /// </summary>
        [Fact]
        public void Curry_FiveArgumentFunc()
        {
            Func<int, int, int, int, int, int> plusFiveFunc =
                (numberFirst, numberSecond, thirdNumber, fourthNumber, fiveNumber) => 
                    numberFirst + numberSecond + thirdNumber + fourthNumber + fiveNumber;

            var totalFunc = plusFiveFunc.Curry(6);

            Assert.Equal(12, totalFunc.Invoke(2, 2, 1, 1));
        }
    }
}