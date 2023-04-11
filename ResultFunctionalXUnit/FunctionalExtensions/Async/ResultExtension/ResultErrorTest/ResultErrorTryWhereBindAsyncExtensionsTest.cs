﻿using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;
using static ResultFunctional.FunctionalExtensions.Sync.RExtension.Units.ResultErrorTryExtensions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа и обработкой исключений. Тесты
    /// </summary>
    public class ResultErrorTryWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ
        /// </summary>
        [Fact]
        public async Task ResultErrorTryWhereBindAsync_Ok()
        {
            int initialValue = Numbers.Number;
            var numberResult = RUnitFactory.SomeTask();

            var resultError = await numberResult.ResultErrorTryOkBindAsync(() => AsyncFunctions.DivisionAsync(initialValue), 
                                                                       Exceptions.ExceptionError());

            Assert.True(resultError.Success);
        }

        /// <summary>
        /// Обработать функцию, вернуть результирующий ответ с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultErrorTryWhereBindAsync_Exception()
        {
            const int initialValue = 0;
            var numberResult = RUnitFactory.SomeTask();

            var resultError = await numberResult.ResultErrorTryOkBindAsync(() => AsyncFunctions.DivisionAsync(initialValue),
                                                                       Exceptions.ExceptionError());

            Assert.True(resultError.Failure);
            Assert.NotNull(resultError.GetErrors().First().Exception);
        }
    }
}