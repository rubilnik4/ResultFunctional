using System.Linq;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists.ResultCollectionTryExtensions;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией и обработкой исключений. Тесты
    /// </summary>
    public class ResultCollectionTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией
        /// </summary>
        [Fact]
        public void ResultCollectionTry_Ok()
        {
            int initialValue = Numbers.Number;

            var resultCollection = ResultCollectionTry(() => DivisionCollection(initialValue), Exceptions.ExceptionError());

            Assert.True(resultCollection.Success);
            Assert.True(DivisionCollection(initialValue).SequenceEqual( resultCollection.GetValue()));
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией
        /// </summary>
        [Fact]
        public void ResultCollectionTry_Exception()
        {
            const int initialValue = 0;

            var resultCollection = ResultCollectionTry(() => DivisionCollection(initialValue), Exceptions.ExceptionError());

            Assert.True(resultCollection.Failure);
            Assert.NotNull(resultCollection.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией
        /// </summary>
        [Fact]
        public void ResultCollectionTryFunc_Ok()
        {
            int initialValue = Numbers.Number;

            var resultCollection = ResultCollectionTry(() => DivisionCollection(initialValue), Exceptions.ExceptionFunc());

            Assert.True(resultCollection.Success);
            Assert.True(DivisionCollection(initialValue).SequenceEqual(resultCollection.GetValue()));
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией
        /// </summary>
        [Fact]
        public void ResultCollectionTryFunc_Exception()
        {
            const int initialValue = 0;

            var resultCollection = ResultCollectionTry(() => DivisionCollection(initialValue), Exceptions.ExceptionFunc());

            Assert.True(resultCollection.Failure);
            Assert.NotNull(resultCollection.GetErrors().First().Exception);
        }
    }
}