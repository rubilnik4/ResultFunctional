using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Units;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Units;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RUnitTest
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа. Тест
    /// </summary>
    public class ResultErrorBindWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        [Fact]
        public async Task ResultErrorBindOkBadAsync_Ok()
        {
            var initialResult = RUnitFactory.Some();
            var addingResult = RUnitFactory.Some();

            var result = await initialResult.ResultErrorBindOkBadAsync(() => addingResult.ToTask(),
                                                                       _ => RUnitFactory.NoneTask(CreateErrorTest()));

            Assert.True(result.Success);
        }

        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        [Fact]
        public async Task ResultErrorBindOkBadAsync_Error()
        {
            var initialResult = CreateErrorListTwoTest().ToRUnit();
            var addingResult = RUnitFactory.Some();
            var addingResultBad = CreateErrorTest().ToRUnit();

            var result = await initialResult.ResultErrorBindOkBadAsync(() => addingResult.ToTask(),
                                                            _ => addingResultBad.ToTask());

            Assert.True(result.Failure);
            Assert.Equal(addingResultBad.GetErrors().Count, result.GetErrors().Count);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkAsync_Ok_NoError()
        {
            var initialResult = RUnitFactory.Some();
            var addingResult = RUnitFactory.SomeTask();

            var result = await initialResult.ResultErrorBindOkAsync(() => addingResult);

            Assert.True(result.Success);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkAsync_Ok_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = RUnitFactory.Some();
            var addingResult = initialError.ToRUnit();

            var result = await initialResult.ResultErrorBindOkAsync(() => addingResult.ToTask());

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(initialError));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkAsync_Bad_NoError()
        {
            var initialError = CreateErrorTest();
            var initialResult = initialError.ToRUnit();
            var addingResult = initialError.ToRUnit();

            var result = await initialResult.ResultErrorBindOkAsync(() => addingResult.ToTask());

            Assert.True(result.Failure);
            Assert.True(result.Equals(initialResult));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkAsync_Bad_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = initialError.ToRUnit();
            var addingResult = initialError.ToRUnit();

            var result = await initialResult.ResultErrorBindOkAsync(() => addingResult.ToTask());

            Assert.True(result.Failure);
            Assert.Single(result.GetErrors());
            Assert.True(result.Equals(initialResult));
        }
    }
}