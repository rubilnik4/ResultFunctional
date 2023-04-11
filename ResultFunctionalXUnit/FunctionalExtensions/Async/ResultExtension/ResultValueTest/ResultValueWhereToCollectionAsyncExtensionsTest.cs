using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.AsyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Обработка асинхронных условий для результирующего ответа с значением с возвращением к коллекции. Тесты
    /// </summary>
    public class ResultValueWhereToCollectionAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public async Task ResultValueContinueToCollectionAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.ResultValueContinueToCollectionAsync(_ => true,
                okFunc: NumberToCollectionAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Success);
            Assert.True((await NumberToCollectionAsync(initialValue)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение асинхронного негативного условия в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public async Task ResultValueContinueToCollectionAsync_Ok_ReturnNewError()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var errorsBad = CreateErrorListTwoTestTask();
            var resultAfterWhere = await resultValue.ResultValueContinueToCollectionAsync(_ => false,
                okFunc: NumberToCollectionAsync,
                badFunc: _ => errorsBad);

            Assert.True(resultAfterWhere.Failure);
            Assert.Equal(errorsBad.Result.Count, resultAfterWhere.GetErrors().Count);
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public async Task ResultValueContinueToCollectionAsync_Bad_ReturnNewValue()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.ResultValueContinueToCollectionAsync(_ => true,
                okFunc: NumberToCollectionAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в отрицательном результирующем ответе значения с возвращением к коллекции
        /// </summary>
        [Fact]
        public async Task ResultValueContinueToCollectionAsync_Bad_ReturnNewError()
        {
            var errorsInitial = CreateErrorTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.ResultValueContinueToCollectionAsync(_ => false,
                okFunc: NumberToCollectionAsync,
                badFunc: _ => CreateErrorListTwoTestTask());

            Assert.True(resultAfterWhere.Failure);
            Assert.Single(resultAfterWhere.GetErrors());
        }

        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе без ошибки
        /// </summary>      
        [Fact]
        public async Task ResultValueOkBadToCollectionAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.ResultValueOkBadToCollectionAsync(
                okFunc: NumberToCollectionAsync,
                badFunc: _ => Task.FromResult((IReadOnlyCollection<int>)new List<int>()));

            Assert.True(resultAfterWhere.Success);
            Assert.True((await NumberToCollectionAsync(initialValue)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Выполнение асинхронного негативного условия в результирующем ответе с ошибкой
        /// </summary>      
        [Fact]
        public async Task ResultValueOkBadToCollectionAsync_Bad_ReturnNewValueByErrors()
        {
            var errorsInitial = CreateErrorListTwoTest();
            var resultValue = errorsInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.ResultValueOkBadToCollectionAsync(
                okFunc: NumberToCollectionAsync,
                badFunc: errors => Task.FromResult((IReadOnlyCollection<int>)new List<int> { errors.Count }));

            Assert.True(resultAfterWhere.Success);
            Assert.Equal(errorsInitial.Count, resultAfterWhere.GetValue().First());
        }

        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueOkToCollectionAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = initialValue.ToRValue();

            var resultAfterWhere = await resultValue.ResultValueOkToCollectionAsync(NumberToCollectionAsync);

            Assert.True(resultAfterWhere.Success);
            Assert.True((await NumberToCollectionAsync(initialValue)).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Возвращение асинхронного предыдущей ошибки в результирующем ответе с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueOkToCollectionAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = errorInitial.ToRValue<int>();

            var resultAfterWhere = await resultValue.ResultValueOkToCollectionAsync(NumberToCollectionAsync);

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }
    }
}