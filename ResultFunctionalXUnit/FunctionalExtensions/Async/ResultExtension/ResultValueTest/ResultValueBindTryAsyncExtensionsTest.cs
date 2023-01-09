using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues.ResultValueBindTryAsyncExtensions;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
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

            var resultValue = await ResultValueBindTryAsync(() => ResultValueFactory.CreateTaskResultValue(Division(initialValue)),
                                                            Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(resultValue.Value, await AsyncFunctions.DivisionAsync(initialValue));
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryAsync_Exception()
        {
            const int initialValue = 0;

            var resultValue = await ResultValueBindTryAsync(() => ResultValueFactory.CreateTaskResultValue(Division(initialValue)),
                                                            Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryAsyncFunc_Ok()
        {
            int initialValue = Numbers.Number;

            var resultValue = await ResultValueBindTryAsync(() => ResultValueFactory.CreateTaskResultValue(Division(initialValue)),
                                                            Exceptions.ExceptionFunc());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(resultValue.Value, await AsyncFunctions.DivisionAsync(initialValue));
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueBindTryAsyncFunc_Exception()
        {
            const int initialValue = 0;

            var resultValue = await ResultValueBindTryAsync(() => ResultValueFactory.CreateTaskResultValue(Division(initialValue)),
                                                            Exceptions.ExceptionFunc());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }
    }
}