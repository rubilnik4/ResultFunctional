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
    public class RListBindTryExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void RListBindTry_Ok()
        {
            int initialValue = Numbers.Number;
            var rList = RListBindTry(() => DivisionCollection(initialValue).ToRList(),
                                                           Exceptions.ExceptionError());

            Assert.True(rList.Success);
            Assert.True(DivisionCollection(initialValue).SequenceEqual(rList.GetValue()));
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void RListBindTry_Exception()
        {
            const int initialValue = 0;
            var RValue = RListBindTry(
                () => DivisionCollection(initialValue).ToRList(), Exceptions.ExceptionError());

            Assert.True(RValue.Failure);
            Assert.NotNull(RValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public void RListBindTryFunc_Ok()
        {
            int initialValue = Numbers.Number;
            var rList = RListBindTry(() => DivisionCollection(initialValue).ToRList(),
                                                           Exceptions.ExceptionFunc());

            Assert.True(rList.Success);
            Assert.True(DivisionCollection(initialValue).SequenceEqual(rList.GetValue()));
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public void RListBindTryFunc_Exception()
        {
            const int initialValue = 0;
            var RValue = RListBindTry(
                () => DivisionCollection(initialValue).ToRList(), Exceptions.ExceptionFunc());

            Assert.True(RValue.Failure);
            Assert.NotNull(RValue.GetErrors().First().Exception);
        }
    }
}