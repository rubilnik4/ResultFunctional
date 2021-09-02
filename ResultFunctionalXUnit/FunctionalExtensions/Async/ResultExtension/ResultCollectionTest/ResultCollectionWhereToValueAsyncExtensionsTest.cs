using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Extensions.TaskExtensions.TaskExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа задачи-объекта с коллекцией с возвращением к значению. Тесты
    /// </summary>
    public class ResultCollectionWhereToValueAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueToValueAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollectionTask = new ResultCollection<int>(numberCollection);

            var resultAfterWhere = await resultCollectionTask.ResultCollectionContinueToValueAsync(_ => true,
                okFunc: Collections.AggregateToStringAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueToValueAsync_Ok_ReturnNewError()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollectionTask = new ResultCollection<int>(numberCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollectionTask.ResultCollectionContinueToValueAsync(_ => false,
                okFunc: Collections.AggregateToStringAsync,
                badFunc: _ => ToTaskEnumerable(errorsBad));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueToValueAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueToValueAsync(_ => true,
                okFunc: Collections.AggregateToStringAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueToValueAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueToValueAsync(_ => false,
                okFunc: Collections.AggregateToStringAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadToValueAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = new ResultCollection<int>(numberCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionOkBadToValueAsync(
                okFunc: Collections.AggregateToStringAsync,
                badFunc: _ => Task.FromResult(String.Empty));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadToValueAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionOkBadToValueAsync(
                okFunc: Collections.AggregateToStringAsync,
                badFunc: errors => Task.FromResult(errors.Count.ToString()));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkToValueAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection =new ResultCollection<int>(numberCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionOkToValueAsync(Collections.AggregateToStringAsync);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkToValueAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionOkToValueAsync(Collections.AggregateToStringAsync);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }
    }
}