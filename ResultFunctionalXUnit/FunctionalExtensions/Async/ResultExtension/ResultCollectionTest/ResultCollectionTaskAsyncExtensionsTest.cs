using System.Collections.Generic;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Interfaces.Results;
using ResultFunctionalXUnit.Data;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать в ответ со значением-коллекцией. Верно
        /// </summary>
        [Fact]
        public async Task ToResultValue_Ok()
        {
            var numbers = Collections.GetRangeNumber();
            var resultCollectionTask = ResultCollectionFactory.CreateTaskResultCollection(numbers);

            var resultValue = await resultCollectionTask.ToResultValueTaskAsync();
            
            Assert.IsAssignableFrom<IResultValue<IReadOnlyCollection<int>>>(resultValue);
        }
    }
}