using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Обработка условий для результирующего ответа с коллекцией с возвращением к значению. Тесты
    /// </summary>
    public class RListToValueBindOptionAwaitExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RListToValueBindOptionAwait_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = numberCollection.ToRList().ToTask();

            var resultAfterWhere = await rList.RListToValueBindOptionAwait(_ => true,
                                                          numbers => RValueFactory.SomeTask(Collections.AggregateToString(numbers)),
                                                          _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(Collections.AggregateToString(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение условия в отрицательном результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RValueToValueBindOptionAwait_Ok_ReturnNewError()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = numberCollection.ToRList().ToTask();

            var errorBad = CreateErrorListTwoTest();
            var resultAfterWhere = await rList.RListToValueBindOptionAwait(_ => false,
                                                          numbers => RValueFactory.SomeTask(Collections.AggregateToString(numbers)),
                                                          _ => errorBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RValueToValueBindOptionAwait_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>().ToTask();

            var resultAfterWhere = await rList.RListToValueBindOptionAwait(_ => true,
                                                          numbers => RValueFactory.SomeTask(Collections.AggregateToString(numbers)),
                                                          _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>
        [Fact]
        public async Task RValueToValueBindOptionAwait_Bad_ReturnNewError()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>().ToTask();

            var resultAfterWhere = await rList.RListToValueBindOptionAwait(_ => false,
                                                          numbers => RValueFactory.SomeTask(Collections.AggregateToString(numbers)),
                                                          _ => CreateErrorListTwoTest());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task RValueToValueBindMatchAwait_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = numberCollection.ToRList().ToTask();

            var resultAfterWhere = await rList.RListToValueBindMatchAwait(numbers => RValueFactory.SomeTask(Collections.AggregateToString(numbers)),
                                                               _ => RValueFactory.SomeTask(String.Empty));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(Collections.AggregateToString(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение негативного условия в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>      
        [Fact]
        public async Task RValueToValueBindMatchAwait_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var rList = errorsInitial.ToRList<int>().ToTask();

            var resultAfterWhere = await rList.RListToValueBindMatchAwait(numbers => RValueFactory.SomeTask(Collections.AggregateToString(numbers)),
                                                               errors => RValueFactory.SomeTask(errors.Count.ToString()));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Выполнение положительного условия в результирующем ответе без ошибки с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task RValueToValueBindSomeAwait_Ok_ReturnNewValue()
        {
            var numberCollection = Collections.GetRangeNumber();
            var rList = numberCollection.ToRList().ToTask();

            var resultAfterWhere = await rList.RListToValueBindSomeAwait(numbers => RValueFactory.SomeTask(Collections.AggregateToString(numbers)));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(Collections.AggregateToString(numberCollection), resultAfterWhere.GetValue());
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в результирующем ответе с ошибкой с коллекцией и возвращением к значению
        /// </summary>   
        [Fact]
        public async Task RValueToValueBindSomeAwait_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rList = errorInitial.ToRList<int>().ToTask();

            var resultAfterWhere = await rList.RListToValueBindSomeAwait(numbers => RValueFactory.SomeTask(Collections.AggregateToString(numbers)));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }
    }
}