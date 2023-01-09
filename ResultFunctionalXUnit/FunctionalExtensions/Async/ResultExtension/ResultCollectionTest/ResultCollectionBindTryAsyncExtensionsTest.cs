using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections.ResultCollectionBindTryTaskAsyncExtensions;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class ResultCollectionBindTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Ok()
        {
            var resultValue = await ResultCollectionBindTryTaskAsync(
                () => ResultCollectionFactory.CreateTaskResultCollection(DivisionCollection(1)), Exceptions.ExceptionError());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(DivisionCollection(1), resultValue.Value);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Exception()
        {
            var resultValue = await ResultCollectionBindTryTaskAsync(
                () => ResultCollectionFactory.CreateTaskResultCollection(DivisionCollection(0)), Exceptions.ExceptionError());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryFunc_Ok()
        {
            var resultValue = await ResultCollectionBindTryTaskAsync(
                () => ResultCollectionFactory.CreateTaskResultCollection(DivisionCollection(1)), Exceptions.ExceptionFunc());

            Assert.True(resultValue.OkStatus);
            Assert.Equal(DivisionCollection(1), resultValue.Value);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryFunc_Exception()
        {
            var resultValue = await ResultCollectionBindTryTaskAsync(
                 () => ResultCollectionFactory.CreateTaskResultCollection(DivisionCollection(0)), Exceptions.ExceptionFunc());

            Assert.True(resultValue.HasErrors);
            Assert.NotNull(resultValue.Errors.First().Exception);
        } }
}