using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа с значением с возвращением к коллекции. Тесты
    /// </summary>
    public class ResultCollectionWhereToCollectionTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного условия в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public void ResultValueContinueToCollection_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere =
                resultValue.ResultValueContinueToCollection(_ => true,
                                                okFunc: NumberToCollection,
                                                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(NumberToCollection(initialValue).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public void ResultValueContinueToCollection_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);
            var errorBad = CreateErrorListTwoTest();

            var resultAfterWhere =
                resultValue.ResultValueContinueToCollection(_ => false,
                                                okFunc: NumberToCollection,
                                                badFunc: _ => errorBad);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public void ResultValueContinueToCollection_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere =
                resultValue.ResultValueContinueToCollection(_ => true,
                                                okFunc: NumberToCollection,
                                                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public void ResultValueContinueToCollection_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere =
                resultValue.ResultValueContinueToCollection(_ => false,
                                                okFunc: NumberToCollection,
                                                badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public void ResultValueOkBadToCollection_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere =
                resultValue.ResultValueOkBadToCollection(
                    okFunc: NumberToCollection,
                    badFunc: _ => new List<int>());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(NumberToCollection(initialValue).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public void ResultValueOkBadToCollection_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultValue<int>(errorsInitial);

            var resultAfterWhere =
                resultValue.ResultValueOkBadToCollection(
                    okFunc: NumberToCollection,
                    badFunc: errors => new List<int> { errors.Count });

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void ResultValueOkToCollection_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = new ResultValue<int>(initialValue);

            var resultAfterWhere = resultValue.ResultValueOkToCollection(NumberToCollection);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(NumberToCollection(initialValue).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void ResultValueOkToCollection_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultValue<int>(errorInitial);

            var resultAfterWhere = resultValue.ResultValueOkToCollection(NumberToCollection);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }
    }
}