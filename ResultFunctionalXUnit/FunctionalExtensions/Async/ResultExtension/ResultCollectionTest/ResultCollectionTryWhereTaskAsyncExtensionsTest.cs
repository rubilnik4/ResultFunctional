using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Lists;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией и обработкой исключений для задачи-объекта. Тесты
    /// </summary>
    public class ResultCollectionTryWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryTaskAsyncOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = RListFactory.SomeTask(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkTaskAsync(DivisionByCollection, Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.Success);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numbersAfterTry.GetValue()));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryTaskAsyncOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = RListFactory.NoneTask<int>(initialError);

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkTaskAsync(DivisionByCollection, Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.Failure);
            Assert.True(initialError.Equals(numbersAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryTaskAsyncOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumber();
            var numberResult = RListFactory.SomeTask(initialNumbers);

            var resultCollection = await numberResult.ResultCollectionTryOkTaskAsync(DivisionCollectionByZero, Exceptions.ExceptionError());

            Assert.True(resultCollection.Failure);
            Assert.NotNull(resultCollection.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryTaskAsyncOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = RListFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numbersResult.ResultCollectionTryOkTaskAsync(DivisionCollectionByZero, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryTaskAsyncOkFunc_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = RListFactory.SomeTask(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkTaskAsync(DivisionByCollection, Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.Success);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numbersAfterTry.GetValue()));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryTaskAsyncOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = RListFactory.NoneTask<int>(initialError);

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkTaskAsync(DivisionByCollection, Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.Failure);
            Assert.True(initialError.Equals(numbersAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryTaskAsyncOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumber();
            var numberResult = RListFactory.SomeTask(initialNumbers);

            var resultCollection = await numberResult.ResultCollectionTryOkTaskAsync(DivisionCollectionByZero, Exceptions.ExceptionFunc());

            Assert.True(resultCollection.Failure);
            Assert.NotNull(resultCollection.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryTaskAsyncOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = RListFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numbersResult.ResultCollectionTryOkTaskAsync(DivisionCollectionByZero, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}