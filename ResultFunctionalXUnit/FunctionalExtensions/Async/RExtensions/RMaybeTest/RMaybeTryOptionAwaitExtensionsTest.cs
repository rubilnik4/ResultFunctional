﻿using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RMaybeTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений. Тесты
    /// </summary>
    public class RMaybeTryOptionAwaitExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ
        /// </summary>
        [Fact]
        public async Task RMaybeTryWhereBindAsync_Ok()
        {
            int initialValue = Numbers.Number;
            var numberResult = RUnitFactory.SomeTask();

            var rMaybe = await numberResult.ToRMaybeTask().RMaybeTrySomeAwait(() => AsyncFunctions.DivisionAsync(initialValue),
                                                                       Exceptions.ExceptionError());

            Assert.True(rMaybe.Success);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task RMaybeTryWhereBindAsync_Exception()
        {
            const int initialValue = 0;
            var numberResult = RUnitFactory.SomeTask();

            var rMaybe = await numberResult.ToRMaybeTask().RMaybeTrySomeAwait(() => AsyncFunctions.DivisionAsync(initialValue),
                                                                       Exceptions.ExceptionError());

            Assert.True(rMaybe.Failure);
            Assert.NotNull(rMaybe.GetErrors().First().Exception);
        }
    }
}