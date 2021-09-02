using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class ResultCollectionWhereExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public void ResultCollectionContinue_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = resultCollection.ResultCollectionContinue(_ => true,
                                                                             okFunc: CollectionToString,
                                                                             badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public void ResultCollectionContinue_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultCollection.ResultCollectionContinue(_ => false,
                                                                             okFunc: _ => String.Empty,
                                                                             badFunc: _ => errorBad);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorBad.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionContinue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = resultCollection.ResultCollectionContinue(_ => true,
                                                                             okFunc: _ => String.Empty,
                                                                             badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionContinue_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = resultCollection.ResultCollectionContinue(_ => false,
                                                                             okFunc: _ => String.Empty,
                                                                             badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Single(resultAfterWhere.Errors);
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public void ResultCollectionWhere_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = resultCollection.ResultCollectionWhere(_ => true,
                                                                          okFunc: CollectionToString,
                                                                          badFunc: _ => new List<string>());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public void ResultCollectionWhere_Ok_ReturnNewValueByErrors()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = resultCollection.ResultCollectionWhere(_ => false,
                                                                          okFunc: _ => new List<string>(),
                                                                          badFunc: numbers => new List<string> { numbers.Count.ToString() });

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Single(resultAfterWhere.Value);
            Assert.Equal(initialCollection.Count.ToString(), resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionWhere_Ok_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = resultCollection.ResultCollectionWhere(_ => true,
                okFunc: _ => new List<string>(),
                badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionWhere_Bad_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = resultCollection.ResultCollectionWhere(_ => false,
                okFunc: _ => new List<string>(),
                badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.HasErrors);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Errors.Count);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе с коллекцией без ошибки
        /// </summary>      
        [Fact]
        public void ResultCollectionOkBad_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = resultCollection.ResultCollectionOkBad(okFunc: CollectionToString,
                                                                          badFunc: _ => new List<string>());

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public void ResultCollectionOkBad_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = resultCollection.ResultCollectionOkBad(
                okFunc: _ => GetEmptyStringList(),
                badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Single(resultAfterWhere.Value);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.Value.First());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void ResultCollectionOk_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = resultCollection.ResultCollectionOk(CollectionToString);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void ResultCollectionOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = new ResultCollection<int>(errorInitial);

            var resultAfterWhere = resultValue.ResultValueOk(CollectionToString);

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void ResultCollectionBad_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = new ResultCollection<int>(initialCollection);

            var resultAfterWhere = resultCollection.ResultCollectionBad(GetListByErrorsCount);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(initialCollection, resultAfterWhere.Value);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void ResultCollectionBad_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = new ResultCollection<int>(errorsInitial);

            var resultAfterWhere = resultValue.ResultCollectionBad(GetListByErrorsCount);

            Assert.True(resultAfterWhere.OkStatus);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.Value.First());
        }
    }
}