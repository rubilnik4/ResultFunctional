using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;
using static ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections.ResultCollectionBindTryExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с со связыванием коллекцией и обработкой исключений. Тесты
    /// </summary>
    public class ResultCollectionBindTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void ResultCollectionBindTry_Ok()
        {
            int initialValue = Numbers.Number;
            var resultCollection = ResultCollectionBindTry(() => new ResultCollection<int>(DivisionCollection(initialValue)),
                                                           Exceptions.ExceptionError());

            Assert.True(resultCollection.OkStatus);
            Assert.True(DivisionCollection(initialValue).SequenceEqual(resultCollection.Value));
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionBindTry_Exception()
        {
            const int initialValue = 0;
            var resultValue = ResultCollectionBindTry(
                () => new ResultCollection<int>(DivisionCollection(initialValue)), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryFunc_Ok()
        {
            int initialValue = Numbers.Number;
            var resultCollection = ResultCollectionBindTry(() => new ResultCollection<int>(DivisionCollection(initialValue)),
                                                           Exceptions.ExceptionFunc());

            Assert.True(resultCollection.OkStatus);
            Assert.True(DivisionCollection(initialValue).SequenceEqual(resultCollection.Value));
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryFunc_Exception()
        {
            const int initialValue = 0;
            var resultValue = ResultCollectionBindTry(
                () => new ResultCollection<int>(DivisionCollection(initialValue)), Exceptions.ExceptionFunc());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }
    }
}