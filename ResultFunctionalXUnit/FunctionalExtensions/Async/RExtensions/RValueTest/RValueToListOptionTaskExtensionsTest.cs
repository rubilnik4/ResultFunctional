using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа с значением с возвращением к коллекции объекта-задачи. Тесты
    /// </summary>
    public class RValueToListOptionTaskExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного условия в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public async Task RValueContinueToCollectionTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueToListOptionTask(_ => true,
                NumberToCollection,
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.True(NumberToCollection(initialValue).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public async Task RValueContinueToCollectionTaskAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = await rValue.RValueToListOptionTask(_ => false,
                NumberToCollection,
                _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public async Task RValueContinueToCollectionTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueToListOptionTask(_ => true,
                NumberToCollection,
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public async Task RValueContinueToCollectionTaskAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueToListOptionTask(_ => false,
                NumberToCollection,
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task RValueOkBadToCollectionTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueToListMatchTask(
                NumberToCollection,
                _ => new List<int>());

            Assert.True(resultAfterWhere.Success);
            Assert.True(NumberToCollection(initialValue).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task RValueOkBadToCollectionTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rValue = RValueFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await rValue.RValueToListMatchTask(
                NumberToCollection,
                errors => new List<int> { errors.Count });

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task RValueOkToCollectionTaskAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueToListSomeTask(NumberToCollection);

            Assert.True(resultAfterWhere.Success);
            Assert.True(NumberToCollection(initialValue).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task RValueOkToCollectionTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueToListSomeTask(NumberToCollection);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }
    }
}