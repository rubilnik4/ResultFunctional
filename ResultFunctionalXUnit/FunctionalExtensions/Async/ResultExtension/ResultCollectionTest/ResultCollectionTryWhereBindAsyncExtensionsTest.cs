using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;
using static ResultFunctionalXUnit.Mocks.Implementation.AsyncFunctions;
using ResultFunctional.Models.Factories;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией и обработкой исключений для задачи-объекта. Тесты
    /// </summary>
    public class ResultCollectionTryWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryBindAsyncOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = ResultCollectionFactory.CreateTaskResultCollection(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkBindAsync(DivisionByCollectionAsync, Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.OkStatus);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numbersAfterTry.Value));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryBindAsyncOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = ResultCollectionFactory.CreateTaskResultCollectionError<int>(initialError);

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkBindAsync(DivisionByCollectionAsync, Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.True(initialError.Equals(numbersAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryBindAsyncOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumber();
            var numberResult = ResultCollectionFactory.CreateTaskResultCollection(initialNumbers);

            var resultCollection = await numberResult.ResultCollectionTryOkBindAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionError());

            Assert.True(resultCollection.HasErrors);
            Assert.NotNull(resultCollection.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryBindAsyncOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = ResultCollectionFactory.CreateTaskResultCollectionError<int>(initialError);

            var numberAfterTry = await numbersResult.ResultCollectionTryOkBindAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryBindAsyncOkFunc_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = ResultCollectionFactory.CreateTaskResultCollection(initialNumbers);

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkBindAsync(DivisionByCollectionAsync, Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.OkStatus);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numbersAfterTry.Value));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryBindAsyncOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = ResultCollectionFactory.CreateTaskResultCollectionError<int>(initialError);

            var numbersAfterTry = await numbersResult.ResultCollectionTryOkBindAsync(DivisionByCollectionAsync, Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.HasErrors);
            Assert.True(initialError.Equals(numbersAfterTry.Errors.First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryBindAsyncOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumber();
            var numberResult = ResultCollectionFactory.CreateTaskResultCollection(initialNumbers);

            var resultCollection = await numberResult.ResultCollectionTryOkBindAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionFunc());

            Assert.True(resultCollection.HasErrors);
            Assert.NotNull(resultCollection.Errors.First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task ResultCollectionTryBindAsyncOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = ResultCollectionFactory.CreateTaskResultCollectionError<int>(initialError);

            var numberAfterTry = await numbersResult.ResultCollectionTryOkBindAsync(DivisionCollectionByZeroAsync, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.HasErrors);
            Assert.True(initialError.Equals(numberAfterTry.Errors.First()));
        }
    }
}