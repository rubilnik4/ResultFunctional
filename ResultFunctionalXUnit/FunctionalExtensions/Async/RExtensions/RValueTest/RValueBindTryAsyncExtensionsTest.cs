using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.RExtensions.Values.RValueBindTryAsyncExtensions;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со связыванием со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class RValueBindTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task RValueBindTryAsync_Ok()
        {
            int initialValue = Numbers.Number;

            var rValue = await RValueBindTryAsync(() => RValueFactory.SomeTask(Division(initialValue)),
                                                            Exceptions.ExceptionError());

            Assert.True(rValue.Success);
            Assert.Equal(rValue.GetValue(), await AsyncFunctions.DivisionAsync(initialValue));
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueBindTryAsync_Exception()
        {
            const int initialValue = 0;

            var rValue = await RValueBindTryAsync(() => RValueFactory.SomeTask(Division(initialValue)),
                                                            Exceptions.ExceptionError());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task RValueBindTryAsyncFunc_Ok()
        {
            int initialValue = Numbers.Number;

            var rValue = await RValueBindTryAsync(() => RValueFactory.SomeTask(Division(initialValue)),
                                                            Exceptions.ExceptionFunc());

            Assert.True(rValue.Success);
            Assert.Equal(rValue.GetValue(), await AsyncFunctions.DivisionAsync(initialValue));
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueBindTryAsyncFunc_Exception()
        {
            const int initialValue = 0;

            var rValue = await RValueBindTryAsync(() => RValueFactory.SomeTask(Division(initialValue)),
                                                            Exceptions.ExceptionFunc());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }
    }
}