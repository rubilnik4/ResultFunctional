﻿using System;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest
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
            var resultCollection = numberCollection.ToRList();

            var resultAfterWhere =
                resultCollection.RListValueOption(_ => true,
                    Collections.AggregateToString,
                    _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(Collections.AggregateToString(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public void ResultValueContinueToValue_Ok_ReturnNewError()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = numberCollection.ToRList();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere =
                resultCollection.RListValueOption(_ => false,
                    _ => String.Empty,
                    _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public void ResultValueContinueToValue_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere =
                resultCollection.RListValueOption(_ => true,
                    _ => String.Empty,
                    _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public void ResultValueContinueToValue_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere =
                resultCollection.RListValueOption(_ => false,
                    _ => String.Empty, 
                    _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public void ResultValueOkBadToValue_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = numberCollection.ToRList();

            var resultAfterWhere =
                resultCollection.RListValueMatch(
                    Collections.AggregateToString,
                    _ => String.Empty);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(Collections.AggregateToString(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public void ResultValueOkBadToValue_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultCollection = errorsInitial.ToRList<int>();

            var resultAfterWhere =
                resultCollection.RListValueMatch(
                    _ => String.Empty,
                    errors => errors.Count.ToString());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public void ResultValueOkToValue_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var resultCollection = numberCollection.ToRList();

            var resultAfterWhere = resultCollection.RListValueSome(Collections.AggregateToString);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(Collections.AggregateToString(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public void ResultValueOkToValue_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultCollection = errorInitial.ToRList<int>();

            var resultAfterWhere = resultCollection.RListValueSome(Collections.AggregateToString);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }
    }
}