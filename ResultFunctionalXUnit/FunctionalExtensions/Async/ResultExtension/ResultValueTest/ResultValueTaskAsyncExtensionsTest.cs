﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckTaskAsync_Ok()
        {
            var initialString = Task.FromResult("NotNull");

            var resultString = await initialString!.ToResultValueNullCheckTaskAsync(CreateErrorTest());

            Assert.True(resultString.OkStatus);
            Assert.Equal(initialString.Result, resultString.Value);
        }

        /// <summary>
        /// Проверить объект на нул для задачи-объекта. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckTaskAsync_ErrorNull()
        {
            var initialString = Task.FromResult<string?>(null);
            var initialError = CreateErrorTest();
            var resultString = await initialString!.ToResultValueNullCheckTaskAsync(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Проверить объект на нул для задачи-объекта. Без ошибок
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckValueTaskAsync_Ok()
        {
            var initialString = Task.FromResult("NotNull");

            var resultString = await initialString!.ToResultValueNullValueCheckTaskAsync(CreateErrorTest());

            Assert.True(resultString.OkStatus);
            Assert.Equal(initialString.Result, resultString.Value);
        }

        /// <summary>
        /// Проверить объект на нул для задачи-объекта. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public async Task ToResultValueNullCheckValueTaskAsync_ErrorNull()
        {
            var initialString = Task.FromResult<string>(null!);
            var initialError = CreateErrorTest();
            var resultString = await initialString.ToResultValueNullValueCheckTaskAsync(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError));
        }

        /// <summary>
        ///  Преобразовать в результирующий ответ коллекции
        /// </summary>
        [Fact]
        public async Task ToResultCollectionTaskAsync()
        {
            var rangeTasks = Enumerable.Range(1, 10).Select(GetTaskNumber);
            var resultCollection = await rangeTasks.ToResultCollectionTaskAsync();

            Assert.True(resultCollection.OkStatus);
            Assert.True(resultCollection.Value.SequenceEqual(Enumerable.Range(1, 10)));
        }

        private static async Task<IResultValue<int>> GetTaskNumber(int number)
        {
            await Task.Delay(1);
            return number.ToResultValue();
        }
    }
}