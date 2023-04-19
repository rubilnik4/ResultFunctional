using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Extensions.TaskExtensions.TaskEnumerableExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа задачи-объекта с коллекцией с возвращением к значению. Тесты
    /// </summary>
    public class RListToValueOptionAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RListContinueToValueAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = numberCollection.ToRList();

            var resultAfterWhere = await rList.RListToValueOptionAsync(_ => true,
                Collections.AggregateToStringAsync,
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RListContinueToValueAsync_Ok_ReturnNewError()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = numberCollection.ToRList();

            var errorsBad = CreateErrorListTwoTest();
            var resultAfterWhere = await rList.RListToValueOptionAsync(_ => false,
                Collections.AggregateToStringAsync,
                _ => ToTaskCollection(errorsBad));

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RListContinueToValueAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = await rList.RListToValueOptionAsync(_ => true,
                Collections.AggregateToStringAsync,
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RListContinueToValueAsync_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = await rList.RListToValueOptionAsync(_ => false,
                Collections.AggregateToStringAsync,
                _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task RListOkBadToValueAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = numberCollection.ToRList();

            var resultAfterWhere = await rList.RListToValueMatchAsync(
                Collections.AggregateToStringAsync,
                _ => Task.FromResult(String.Empty));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task RListOkBadToValueAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = errorsInitial.ToRList<int>();

            var resultAfterWhere = await rList.RListToValueMatchAsync(
                Collections.AggregateToStringAsync,
                errors => Task.FromResult(errors.Count.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task RListOkToValueAsync_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = numberCollection.ToRList();

            var resultAfterWhere = await rList.RListToValueSomeAsync(Collections.AggregateToStringAsync);

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(await Collections.AggregateToStringAsync(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task RListOkToValueAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>();

            var resultAfterWhere = await rList.RListToValueSomeAsync(Collections.AggregateToStringAsync);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }
    }
}