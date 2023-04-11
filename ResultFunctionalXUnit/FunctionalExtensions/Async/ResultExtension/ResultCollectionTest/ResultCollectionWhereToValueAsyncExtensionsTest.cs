using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Extensions.TaskExtensions.TaskEnumerableExtensions;

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
            var resultCollection = numberCollection.ToRList();

            var resultAfterWhere = await resultCollection.ResultCollectionContinueToValueAsync(_ => true,
                okFunc: Collections.AggregateToStringAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueToValueAsync_Ok_ReturnNewError()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = numberCollection.ToRList();

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionContinueToValueAsync(_ => false,
                okFunc: Collections.AggregateToStringAsync,
                badFunc: _ => ToTaskCollection(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueToValueAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere = await resultCollection.ResultCollectionContinueToValueAsync(_ => true,
                okFunc: Collections.AggregateToStringAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueToValueAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere = await resultCollection.ResultCollectionContinueToValueAsync(_ => false,
                okFunc: Collections.AggregateToStringAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadToValueAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = numberCollection.ToRList();

            var resultAfterWhere = await resultCollection.ResultCollectionOkBadToValueAsync(
                okFunc: Collections.AggregateToStringAsync,
                badFunc: _ => Task.FromResult(String.Empty));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadToValueAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = await resultCollection.ResultCollectionOkBadToValueAsync(
                okFunc: Collections.AggregateToStringAsync,
                badFunc: errors => Task.FromResult(errors.Count.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkToValueAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = numberCollection.ToRList();

            var resultAfterWhere = await resultCollection.ResultCollectionOkToValueAsync(Collections.AggregateToStringAsync);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkToValueAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere = await resultCollection.ResultCollectionOkToValueAsync(Collections.AggregateToStringAsync);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }
    }
}