using System;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием. Тесты
    /// </summary>
    public class ToResultValueWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhere_Ok()
        {
            const int number = 1;

            var result = number.ToRValueOption(_ => true,
                                                   _ => CreateErrorTest());

            Assert.True(result.Success);
            Assert.Equal(number, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhere_BadError()
        {
            const int number = 1;
            var errorInitial = CreateErrorTest();
            var result = number.ToRValueOption(_ => false,
                                                   _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }
    }
}