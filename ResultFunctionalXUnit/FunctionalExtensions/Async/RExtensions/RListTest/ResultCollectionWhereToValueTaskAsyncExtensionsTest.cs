using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа задачи-объекта с коллекцией с возвращением к значению. Тесты
    /// </summary>
    public class RListWhereToValueTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RListContinueToValueTaskAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var RList = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await RList.RListValueOptionTask(_ => true,
                Collections.AggregateToString,
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RListContinueToValueTaskAsync_Ok_ReturnNewError()
        {
            var numberCollection = Collections.GetRangeNumber();
            var RList = RListFactory.SomeTask(numberCollection);

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await RList.RListValueOptionTask(_ => false,
                _ => String.Empty,
                _ => errorsBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RListContinueToValueTaskAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var RList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await RList.RListValueOptionTask(_ => true,
                _ => String.Empty,
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RListContinueToValueTaskAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var RList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await RList.RListValueOptionTask(_ => false,
                _ => String.Empty,
                _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task RListOkBadToValueTaskAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var RList = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await RList.RListValueMatchTask(
                Collections.AggregateToString,
                _ => String.Empty);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task RListOkBadToValueTaskAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var RList = RListFactory.NoneTask<int>(errorsInitial);

            var resultAfterWhere = await RList.RListValueMatchTask(
                _ => String.Empty,
                errors => errors.Count.ToString());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task RListOkToValueTaskAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var RList = RListFactory.SomeTask(numberCollection);

            var resultAfterWhere = await RList.RListValueSomeTask(Collections.AggregateToString);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task RListOkToValueTaskAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var RList = RListFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await RList.RListValueSomeTask(Collections.AggregateToString);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }
    }
}