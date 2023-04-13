using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;
using static ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists.RListBindTryExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest
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
            var resultCollection = RListBindTry(() => DivisionCollection(initialValue).ToRList(),
                                                           Exceptions.ExceptionError());

            Assert.True(resultCollection.Success);
            Assert.True(DivisionCollection(initialValue).SequenceEqual(resultCollection.GetValue()));
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionBindTry_Exception()
        {
            const int initialValue = 0;
            var resultValue = RListBindTry(
                () => DivisionCollection(initialValue).ToRList(), Exceptions.ExceptionError());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryFunc_Ok()
        {
            int initialValue = Numbers.Number;
            var resultCollection = RListBindTry(() => DivisionCollection(initialValue).ToRList(),
                                                           Exceptions.ExceptionFunc());

            Assert.True(resultCollection.Success);
            Assert.True(DivisionCollection(initialValue).SequenceEqual(resultCollection.GetValue()));
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void ResultCollectionBindTryFunc_Exception()
        {
            const int initialValue = 0;
            var resultValue = RListBindTry(
                () => DivisionCollection(initialValue).ToRList(), Exceptions.ExceptionFunc());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }
    }
}