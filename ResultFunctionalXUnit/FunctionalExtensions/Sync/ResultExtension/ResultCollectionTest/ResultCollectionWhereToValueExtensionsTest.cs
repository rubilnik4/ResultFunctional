using System;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа с коллекцией с возвращением к значению. Тесты
    /// </summary>
    public class ResultCollectionWhereToValueExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public void ResultCollectionContinueToValue_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = new ResultCollection<int>(numberCollection);

            var resultAfterWhere =
                resultCollection.ResultCollectionContinueToValue(_ => true,
                    okFunc: Collections.AggregateToString,
                    badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(Collections.AggregateToString(numberCollection), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public void ResultValueContinueToValue_Ok_ReturnNewError()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = new ResultCollection<int>(numberCollection);

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere =
                resultCollection.ResultCollectionContinueToValue(_ => false,
                    okFunc: _ => String.Empty,
                    badFunc: _ => errorBad);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public void ResultValueContinueToValue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere =
                resultCollection.ResultCollectionContinueToValue(_ => true,
                    okFunc: _ => String.Empty,
                    badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public void ResultValueContinueToValue_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere =
                resultCollection.ResultCollectionContinueToValue(_ => false,
                    okFunc: _ => String.Empty, 
                    badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public void ResultValueOkBadToValue_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = new ResultCollection<int>(numberCollection);

            var resultAfterWhere =
                resultCollection.ResultCollectionOkBadToValue(
                    okFunc: Collections.AggregateToString,
                    badFunc: _ => String.Empty);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(Collections.AggregateToString(numberCollection), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public void ResultValueOkBadToValue_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere =
                resultCollection.ResultCollectionOkBadToValue(
                    okFunc: _ => String.Empty,
                    badFunc: errors => errors.Count.ToString());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public void ResultValueOkToValue_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = new ResultCollection<int>(numberCollection);

            var resultAfterWhere = resultCollection.ResultCollectionOkToValue(Collections.AggregateToString);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(Collections.AggregateToString(numberCollection), resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public void ResultValueOkToValue_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = resultCollection.ResultCollectionOkToValue(Collections.AggregateToString);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }
    }
}