using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Lists;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.Collections;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;
using static ResultFunctionalXUnit.Mocks.Implementation.AsyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RListTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа с коллекцией и обработкой исключений для задачи-объекта. Тесты
    /// </summary>
    public class RListTryWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RListTryBindAsyncOk_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = RListFactory.SomeTask(initialNumbers);

            var numbersAfterTry = await numbersResult.RListTrySomeAwait(DivisionByCollectionAsync, Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.Success);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numbersAfterTry.GetValue()));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RListTryBindAsyncOk_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = RListFactory.NoneTask<int>(initialError);

            var numbersAfterTry = await numbersResult.RListTrySomeAwait(DivisionByCollectionAsync, Exceptions.ExceptionError());

            Assert.True(numbersAfterTry.Failure);
            Assert.True(initialError.Equals(numbersAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RListTryBindAsyncOk_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumber();
            var numberResult = RListFactory.SomeTask(initialNumbers);

            var RList = await numberResult.RListTrySomeAwait(DivisionCollectionByZeroAsync, Exceptions.ExceptionError());

            Assert.True(RList.Failure);
            Assert.NotNull(RList.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RListTryBindAsyncOk_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = RListFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numbersResult.RListTrySomeAwait(DivisionCollectionByZeroAsync, Exceptions.ExceptionError());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RListTryBindAsyncOkFunc_OkResult_OkTry()
        {
            var initialNumbers = GetRangeNumber();
            var numbersResult = RListFactory.SomeTask(initialNumbers);

            var numbersAfterTry = await numbersResult.RListTrySomeAwait(DivisionByCollectionAsync, Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.Success);
            Assert.True(DivisionByCollection(initialNumbers).SequenceEqual(numbersAfterTry.GetValue()));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и отсутствие исключения для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RListTryBindAsyncOkFunc_ErrorResult_OkTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = RListFactory.NoneTask<int>(initialError);

            var numbersAfterTry = await numbersResult.RListTrySomeAwait(DivisionByCollectionAsync, Exceptions.ExceptionFunc());

            Assert.True(numbersAfterTry.Failure);
            Assert.True(initialError.Equals(numbersAfterTry.GetErrors().First()));
        }

        /// <summary>
        /// Положительный результирующий ответ и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RListTryBindAsyncOkFunc_OkResult_ExceptionTry()
        {
            var initialNumbers = GetRangeNumber();
            var numberResult = RListFactory.SomeTask(initialNumbers);

            var RList = await numberResult.RListTrySomeAwait(DivisionCollectionByZeroAsync, Exceptions.ExceptionFunc());

            Assert.True(RList.Failure);
            Assert.NotNull(RList.GetErrors().First().Exception);
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и исключение для задачи-объекта
        /// </summary>
        [Fact]
        public async Task RListTryBindAsyncOkFunc_ErrorResult_ExceptionTry()
        {
            var initialError = CreateErrorTest();
            var numbersResult = RListFactory.NoneTask<int>(initialError);

            var numberAfterTry = await numbersResult.RListTrySomeAwait(DivisionCollectionByZeroAsync, Exceptions.ExceptionFunc());

            Assert.True(numberAfterTry.Failure);
            Assert.True(initialError.Equals(numberAfterTry.GetErrors().First()));
        }
    }
}