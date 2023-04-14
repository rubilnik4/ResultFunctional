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
    public class ResultValueBindTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryAsync_Ok()
        {
            int initialValue = Numbers.Number;

            var resultValue = await RValueBindTryAsync(() => RValueFactory.SomeTask(Division(initialValue)),
                                                            Exceptions.ExceptionError());

            Assert.True(resultValue.Success);
            Assert.Equal(resultValue.GetValue(), await AsyncFunctions.DivisionAsync(initialValue));
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryAsync_Exception()
        {
            const int initialValue = 0;

            var resultValue = await RValueBindTryAsync(() => RValueFactory.SomeTask(Division(initialValue)),
                                                            Exceptions.ExceptionError());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryAsyncFunc_Ok()
        {
            int initialValue = Numbers.Number;

            var resultValue = await RValueBindTryAsync(() => RValueFactory.SomeTask(Division(initialValue)),
                                                            Exceptions.ExceptionFunc());

            Assert.True(resultValue.Success);
            Assert.Equal(resultValue.GetValue(), await AsyncFunctions.DivisionAsync(initialValue));
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryAsyncFunc_Exception()
        {
            const int initialValue = 0;

            var resultValue = await RValueBindTryAsync(() => RValueFactory.SomeTask(Division(initialValue)),
                                                            Exceptions.ExceptionFunc());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }
    }
}