using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Преобразование значений в результирующий ответ коллекции с условием. Тесты
    /// </summary>
    public class ToRListWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToRListWhereBindAsync_Ok()
        {
            var initialCollection = GetRangeNumber();
            var taskCollection = Task.FromResult(GetRangeNumber());

            var result = await taskCollection.ToRListOptionAwait(_ => true,
                                                                                _ => CreateErrorTestTask());

            Assert.True(result.Success);
            Assert.True(initialCollection.SequenceEqual(result.GetValue()));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToRListWhereBindAsync_BadError()
        {
            var initialCollection = GetRangeNumber();
            var taskCollection = Task.FromResult(initialCollection);
            var errorInitial = CreateErrorTest();

            var result = await taskCollection.ToRListOptionAwait(_ => false,
                                                                               _ => Task.FromResult(errorInitial));

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }
    }
}