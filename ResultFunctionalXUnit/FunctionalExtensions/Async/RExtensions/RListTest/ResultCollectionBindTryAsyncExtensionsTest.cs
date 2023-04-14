﻿using System.Linq;
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
    public class ResultCollectionBindTryAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Ok()
        {
            var resultValue = await RListBindTryAsync(
                () => RListFactory.SomeTask(DivisionCollection(1)), Exceptions.ExceptionError());

            Assert.True(resultValue.Success);
            Assert.Equal(DivisionCollection(1), resultValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionTry_Exception()
        {
            var resultValue = await RListBindTryAsync(
                () => RListFactory.SomeTask(DivisionCollection(0)), Exceptions.ExceptionError());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ со значением
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryFunc_Ok()
        {
            var resultValue = await RListBindTryAsync(
                () => RListFactory.SomeTask(DivisionCollection(1)), Exceptions.ExceptionFunc());

            Assert.True(resultValue.Success);
            Assert.Equal(DivisionCollection(1), resultValue.GetValue());
        }

        /// <summary>
        /// Обработать асинхронную функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryFunc_Exception()
        {
            var resultValue = await RListBindTryAsync(
                 () => RListFactory.SomeTask(DivisionCollection(0)), Exceptions.ExceptionFunc());

            Assert.True(resultValue.Failure);
            Assert.NotNull(resultValue.GetErrors().First().Exception);
        } }
}