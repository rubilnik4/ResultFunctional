using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Implementations.ResultFactory;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Преобразование значений в результирующий ответ коллекции с условием. Тесты
    /// </summary>
    public class ToResultCollectionWhereAsyncTaskExtensionsTest
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultCollectionWhere_Ok()
        {
            var initialCollection = GetRangeNumber();
            var taskCollection = Task.FromResult(GetRangeNumber());

            var result = await taskCollection.ToResultCollectionWhereTaskAsync(_ => true,
                                                                                _ => CreateErrorTest());

            Assert.True(result.OkStatus);
            Assert.True(initialCollection.SequenceEqual(result.Value));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultCollectionWhere_BadError()
        {
            var initialCollection = GetRangeNumber();
            var taskCollection = Task.FromResult(GetRangeNumber());
            var errorInitial = CreateErrorTest();

            var result = await taskCollection.ToResultCollectionWhereTaskAsync(_ => false,
                                                                               _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }
    }
}