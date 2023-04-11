using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа задачи-объекта с коллекцией с возвращением к значению. Тесты
    /// </summary>
    public class ResultCollectionWhereToValueTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueToValueTaskAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueToValueTaskAsync(_ => true,
                okFunc: Collections.AggregateToString,
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueToValueTaskAsync_Ok_ReturnNewError()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(numberCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await resultCollection.ResultCollectionContinueToValueTaskAsync(_ => false,
                okFunc: _ => String.Empty,
                badFunc: _ => errorsBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueToValueTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueToValueTaskAsync(_ => true,
                okFunc: _ => String.Empty,
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task ResultCollectionContinueToValueTaskAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionContinueToValueTaskAsync(_ => false,
                okFunc: _ => String.Empty,
                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadToValueTaskAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionOkBadToValueTaskAsync(
                okFunc: Collections.AggregateToString,
                badFunc: _ => String.Empty);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task ResultCollectionOkBadToValueTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionOkBadToValueTaskAsync(
                okFunc: _ => String.Empty,
                badFunc: errors => errors.Count.ToString());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkToValueTaskAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await resultCollection.ResultCollectionOkToValueTaskAsync(Collections.AggregateToString);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task ResultCollectionOkToValueTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultCollection.ResultCollectionOkToValueTaskAsync(Collections.AggregateToString);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }
    }
}