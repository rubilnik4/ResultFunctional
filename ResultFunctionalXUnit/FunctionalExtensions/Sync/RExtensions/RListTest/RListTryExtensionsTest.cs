using System.Linq;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists.RListTryExtensions;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RListTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией и обработкой исключений. Тесты
    /// </summary>
    public class RListTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией
        /// </summary>
        [Fact]
        public void RListTry_Ok()
        {
            int initialValue = Numbers.Number;

            var rList = RListTry(() => DivisionCollection(initialValue), Exceptions.ExceptionError());

            Assert.True(rList.Success);
            Assert.True(DivisionCollection(initialValue).SequenceEqual( rList.GetValue()));
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией
        /// </summary>
        [Fact]
        public void RListTry_Exception()
        {
            const int initialValue = 0;

            var rList = RListTry(() => DivisionCollection(initialValue), Exceptions.ExceptionError());

            Assert.True(rList.Failure);
            Assert.NotNull(rList.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией
        /// </summary>
        [Fact]
        public void RListTryFunc_Ok()
        {
            int initialValue = Numbers.Number;

            var rList = RListTry(() => DivisionCollection(initialValue), Exceptions.ExceptionFunc());

            Assert.True(rList.Success);
            Assert.True(DivisionCollection(initialValue).SequenceEqual(rList.GetValue()));
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с коллекцией
        /// </summary>
        [Fact]
        public void RListTryFunc_Exception()
        {
            const int initialValue = 0;

            var rList = RListTry(() => DivisionCollection(initialValue), Exceptions.ExceptionFunc());

            Assert.True(rList.Failure);
            Assert.NotNull(rList.GetErrors().First().Exception);
        }
    }
}