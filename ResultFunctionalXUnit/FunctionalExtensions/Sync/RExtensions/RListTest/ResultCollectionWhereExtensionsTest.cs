using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest
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
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = resultCollection.ResultCollectionContinue(_ => true,
                                                                             okFunc: CollectionToString,
                                                                             badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public void ResultCollectionContinue_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultCollection.ResultCollectionContinue(_ => false,
                                                                             okFunc: CollectionToString,
                                                                             badFunc: _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionContinue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionContinue(_ => true,
                                                                             okFunc: CollectionToString,
                                                                             badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionContinue_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionContinue(_ => false,
                                                                             okFunc: CollectionToString,
                                                                             badFunc: _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public void ResultCollectionWhere_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = resultCollection.ResultCollectionWhere(_ => true,
                                                                          okFunc: CollectionToString,
                                                                          badFunc: _ => new List<string>());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public void ResultCollectionWhere_Ok_ReturnNewValueByErrors()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = resultCollection.ResultCollectionWhere(_ => false,
                                                                          okFunc: _ => new List<string>(),
                                                                          badFunc: numbers => new List<string> { numbers.Count.ToString() });

            Assert.True(resultAfterWhere.Success);
            Assert.Single(resultAfterWhere.GetValue());
            Assert.Equal(initialCollection.Count.ToString(), resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionWhere_Ok_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionWhere(_ => true,
                okFunc: _ => new List<string>(),
                badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionWhere_Bad_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionWhere(_ => false,
                okFunc: _ => new List<string>(),
                badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе с коллекцией без ошибки
        /// </summary>      
        [Fact]
        public void ResultCollectionOkBad_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = resultCollection.ResultCollectionOkBad(okFunc: CollectionToString,
                                                                          badFunc: _ => new List<string>());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public void ResultCollectionOkBad_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionOkBad(
                okFunc: _ => GetEmptyStringList(),
                badFunc: errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.Success);
            Assert.Single(resultAfterWhere.GetValue());
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void ResultCollectionOk_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = resultCollection.ResultCollectionOk(CollectionToString);

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void ResultCollectionOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRList<int>();

            var resultAfterWhere = resultValue.ResultCollectionOk(CollectionToString);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void ResultCollectionBad_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = resultCollection.ResultCollectionBad(GetListByErrorsCount);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialCollection, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void ResultCollectionBad_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRList<int>();

            var resultAfterWhere = resultValue.ResultCollectionBad(GetListByErrorsCount);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public void ResultCollectionCheckErrorsOk_Ok_CheckNoError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var resultAfterWhere = resultCollection.ResultCollectionCheckErrorsOk(_ => true,
                                                                                  _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public void ResultCollectionCheckErrorsOk_Ok_CheckError()
        {
            var initialCollection = GetRangeNumber();
            var resultCollection = initialCollection.ToRList();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = resultCollection.ResultCollectionCheckErrorsOk(_ => false,
                                                                                  _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionCheckErrorsOk_Bad_CheckNoError()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionCheckErrorsOk(_ => true,
                                                                             _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionCheckErrorsOk_Bad_CheckError()
        {
            var errorsInitial = CreateErrorTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.ResultCollectionCheckErrorsOk(_ => false,
                                                                             _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }
    }
}