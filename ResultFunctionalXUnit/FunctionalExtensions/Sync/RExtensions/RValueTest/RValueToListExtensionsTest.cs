using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа с значением с возвращением к коллекции. Тесты
    /// </summary>
    public class RValueToListExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного условия в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public void RValueContinueToCollection_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere =
                rValue.RValueToListOption(_ => true,
                                                NumberToCollection,
                                                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True(NumberToCollection(initialValue).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public void RValueContinueToCollection_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();
            var errorBad = CreateErrorListTwoTest();

            var resultAfterWhere =
                rValue.RValueToListOption(_ => false,
                                                NumberToCollection,
                                                _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public void RValueContinueToCollection_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere =
                rValue.RValueToListOption(_ => true,
                                                NumberToCollection,
                                                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public void RValueContinueToCollection_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere =
                rValue.RValueToListOption(_ => false,
                                                NumberToCollection,
                                                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public void RValueOkBadToCollection_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere =
                rValue.RValueToListMatch(
                    NumberToCollection,
                    _ => new List<int>());

            Assert.True(resultAfterWhere.Success);
            Assert.True(NumberToCollection(initialValue).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public void RValueOkBadToCollection_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere =
                rValue.RValueToListMatch(
                    NumberToCollection,
                    errors => new List<int> { errors.Count });

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public void RValueOkToCollection_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = initialValue.ToRValue();

            var resultAfterWhere = rValue.RValueToListSome(NumberToCollection);

            Assert.True(resultAfterWhere.Success);
            Assert.True(NumberToCollection(initialValue).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public void RValueOkToCollection_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = rValue.RValueToListSome(NumberToCollection);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }
    }
}