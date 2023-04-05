using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.Models.Implementations.ResultFactory;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Преобразование значений в результирующий ответ коллекции с условием. Тесты
    /// </summary>
    public class ToResultCollectionWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultCollectionWhereBindAsync_Ok()
        {
            var initialCollection = GetRangeNumber();
            var taskCollection = Task.FromResult(GetRangeNumber());

            var result = await taskCollection.ToResultCollectionWhereBindAsync(_ => true,
                                                                                _ => CreateErrorTestTask());

            Assert.True(result.OkStatus);
            Assert.True(initialCollection.SequenceEqual(result.Value));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultCollectionWhereBindAsync_BadError()
        {
            var initialCollection = GetRangeNumber();
            var taskCollection = Task.FromResult(initialCollection);
            var errorInitial = CreateErrorTest();

            var result = await taskCollection.ToResultCollectionWhereBindAsync(_ => false,
                                                                               _ => Task.FromResult(errorInitial));

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }
    }
}