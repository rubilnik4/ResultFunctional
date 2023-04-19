using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Units;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RMaybeTest
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа. Тест
    /// </summary>
    public class RMaybeBindOptionAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        [Fact]
        public async Task RMaybeBindOkBadAsync_Ok()
        {
            var initialResult = RUnitFactory.Some();
            var addingResult = RUnitFactory.Some();

            var result = await initialResult.RMaybeBindMatchAsync(() => addingResult.ToTask().ToRMaybeTask(),
                                                                  _ => RUnitFactory.NoneTask(CreateErrorTest()).ToRMaybeTask());

            Assert.True(result.Success);
        }

        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        [Fact]
        public async Task RMaybeBindOkBadAsync_Error()
        {
            var initialResult = CreateErrorListTwoTest().ToRUnit();
            var addingResult = RUnitFactory.Some();
            var addingResultBad = CreateErrorTest().ToRUnit();

            var result = await initialResult.RMaybeBindMatchAsync(() => addingResult.ToTask().ToRMaybeTask(),
                                                            _ => addingResultBad.ToTask().ToRMaybeTask());

            Assert.True(result.Failure);
            Assert.Equal(addingResultBad.GetErrors().Count, result.GetErrors().Count);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task RMaybeBindOkAsync_Ok_NoError()
        {
            var initialResult = RUnitFactory.Some();
            var addingResult = RUnitFactory.SomeTask();

            var result = await initialResult.RMaybeBindSomeAsync(() => addingResult.ToRMaybeTask());

            Assert.True(result.Success);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task RMaybeBindOkAsync_Ok_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = RUnitFactory.Some();
            var addingResult = initialError.ToRUnit();

            var result = await initialResult.RMaybeBindSomeAsync(() => addingResult.ToTask().ToRMaybeTask());

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(initialError));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task RMaybeBindOkAsync_Bad_NoError()
        {
            var initialError = CreateErrorTest();
            var initialResult = initialError.ToRUnit();
            var addingResult = initialError.ToRUnit();

            var result = await initialResult.RMaybeBindSomeAsync(() => addingResult.ToTask().ToRMaybeTask());

            Assert.True(result.Failure);
            Assert.True(result.Equals(initialResult));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task RMaybeBindOkAsync_Bad_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = initialError.ToRUnit();
            var addingResult = initialError.ToRUnit();

            var result = await initialResult.RMaybeBindSomeAsync(() => addingResult.ToTask().ToRMaybeTask());

            Assert.True(result.Failure);
            Assert.Single(result.GetErrors());
            Assert.True(result.Equals(initialResult));
        }
    }
}