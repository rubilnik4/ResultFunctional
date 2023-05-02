using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием асинхронно. Тесты
    /// </summary>
    public class ToRValueOptionAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToRValueWhereAsync_Ok()
        {
            const int number = 1;

            var result = await number.ToRValueOptionAsync(_ => true,
                                                              _ => CreateErrorTestTask());

            Assert.True(result.Success);
            Assert.Equal(number, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToRValueWhereAsync_BadError()
        {
            const int number = 1;
            var errorInitial = CreateErrorTest();
            var result = await number.ToRValueOptionAsync(_ => false,
                                                              _ => Task.FromResult(errorInitial));

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }
    }
}