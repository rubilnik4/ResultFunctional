using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists.RListBindTryAsyncExtensions;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением и обработкой исключений асинхронно. Тесты
    /// </summary>
    public class RListBindTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task RListTry_Ok()
        {
            var rValue = await RListBindTryAsync(
                () => RListFactory.SomeTask(DivisionCollection(1)), Exceptions.ExceptionError());

            Assert.True(rValue.Success);
            Assert.Equal(DivisionCollection(1), rValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task RListTry_Exception()
        {
            var rValue = await RListBindTryAsync(
                () => RListFactory.SomeTask(DivisionCollection(0)), Exceptions.ExceptionError());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task RListTryFunc_Ok()
        {
            var rValue = await RListBindTryAsync(
                () => RListFactory.SomeTask(DivisionCollection(1)), Exceptions.ExceptionFunc());

            Assert.True(rValue.Success);
            Assert.Equal(DivisionCollection(1), rValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task RListTryFunc_Exception()
        {
            var rValue = await RListBindTryAsync(
                 () => RListFactory.SomeTask(DivisionCollection(0)), Exceptions.ExceptionFunc());

            Assert.True(rValue.Failure);
            Assert.NotNull(rValue.GetErrors().First().Exception);
        } }
}