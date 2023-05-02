using System;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа с коллекцией с возвращением к значению. Тесты
    /// </summary>
    public class RListToValueBindOptionExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public void RListToValueBindOption_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = numberCollection.ToRList();

            var resultAfterWhere = rList.RListToValueBindOption(_ => true,
                                                          numbers => Collections.AggregateToString(numbers).ToRValue(),
                                                          _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(Collections.AggregateToString(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public void RValueToValueBindOption_Ok_ReturnNewError()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = numberCollection.ToRList();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = rList.RListToValueBindOption(_ => false,
                                                          numbers => Collections.AggregateToString(numbers).ToRValue(),
                                                          _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public void RValueToValueBindOption_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = rList.RListToValueBindOption(_ => true,
                                                          numbers => Collections.AggregateToString(numbers).ToRValue(),
                                                          _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public void RValueToValueBindOption_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = rList.RListToValueBindOption(_ => false,
                                                          numbers => Collections.AggregateToString(numbers).ToRValue(),
                                                          _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public void RValueToValueBindMatch_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = numberCollection.ToRList();

            var resultAfterWhere = rList.RListToValueBindMatch(numbers => Collections.AggregateToString(numbers).ToRValue(),
                                                               _ => String.Empty.ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(Collections.AggregateToString(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public void RValueToValueBindMatch_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = rList.RListToValueBindMatch(numbers => Collections.AggregateToString(numbers).ToRValue(),
                                                               errors => errors.Count.ToString().ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public void RValueToValueBindSome_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = numberCollection.ToRList();

            var resultAfterWhere = rList.RListToValueBindSome(numbers => Collections.AggregateToString(numbers).ToRValue());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(Collections.AggregateToString(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public void RValueToValueBindSome_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = rList.RListToValueBindSome(numbers => Collections.AggregateToString(numbers).ToRValue());

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }
    }
}