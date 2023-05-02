using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа с коллекцией. Тесты
    /// </summary>
    public class RListOptionExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public void RListContinue_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = rList.RListOption(_ => true,
                                                                             CollectionToString,
                                                                             _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public void RListContinue_Ok_ReturnNewError()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = rList.RListOption(_ => false,
                                                                             CollectionToString,
                                                                             _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void RListContinue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = rList.RListOption(_ => true,
                                                                             CollectionToString,
                                                                             _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void RListContinue_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = rList.RListOption(_ => false,
                                                                             CollectionToString,
                                                                             _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public void RListWhere_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = rList.RListWhere(_ => true,
                                                                          CollectionToString,
                                                                          _ => new List<string>());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public void RListWhere_Ok_ReturnNewValueByErrors()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = rList.RListWhere(_ => false,
                                                                          _ => new List<string>(),
                                                                          numbers => new List<string> { numbers.Count.ToString() });

            Assert.True(resultAfterWhere.Success);
            Assert.Single(resultAfterWhere.GetValue());
            Assert.Equal(initialCollection.Count.ToString(), resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void RListWhere_Ok_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = rList.RListWhere(_ => true,
                _ => new List<string>(),
                errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void RListWhere_Bad_ReturnError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = rList.RListWhere(_ => false,
                _ => new List<string>(),
                errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе с коллекцией без ошибки
        /// </summary>      
        [Fact]
        public void RListOkBad_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = rList.RListMatch(CollectionToString,
                                                                          _ => new List<string>());

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public void RListOkBad_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = rList.RListMatch(
                _ => GetEmptyStringList(),
                errors => new List<string> { errors.Count.ToString() });

            Assert.True(resultAfterWhere.Success);
            Assert.Single(resultAfterWhere.GetValue());
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void RListOk_Ok_ReturnNewValue()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = rList.RListSome(CollectionToString);

            Assert.True(resultAfterWhere.Success);
            Assert.True(CollectionToString(initialCollection).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void RListOk_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRList<int>();

            var resultAfterWhere = rValue.RListSome(CollectionToString);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }

        /// <summary>
        /// Выполнение отрицательного условия в результирующем ответе с коллекцией без ошибки
        /// </summary>   
        [Fact]
        public void RListBad_Ok_ReturnInitial()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = rList.RListNone(GetListByErrorsCount);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(initialCollection, resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с коллекцией с ошибкой
        /// </summary>   
        [Fact]
        public void RListBad_Bad_ReturnNewValueByError()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = errorsInitial.ToRList<int>();

            var resultAfterWhere = rValue.RListNone(GetListByErrorsCount);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией
        /// </summary>
        [Fact]
        public void RListCheckErrorsOk_Ok_CheckNoError()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var resultAfterWhere = rList.RListEnsure(_ => true,
                                                                                  _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True(initialCollection.SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе с коллекцией без ошибки
        /// </summary>
        [Fact]
        public void RListCheckErrorsOk_Ok_CheckError()
        {
            var initialCollection = GetRangeNumber();
            var rList = initialCollection.ToRList();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = rList.RListEnsure(_ => false,
                                                                                  _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void RListCheckErrorsOk_Bad_CheckNoError()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = rList.RListEnsure(_ => true,
                                                                             _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с коллекцией с ошибкой
        /// </summary>
        [Fact]
        public void RListCheckErrorsOk_Bad_CheckError()
        {
            var errorsInitial = CreateErrorTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = rList.RListEnsure(_ => false,
                                                                             _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }
    }
}