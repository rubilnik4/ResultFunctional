using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Units;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RUnitTest
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа для задачи-объекта. Тест
    /// </summary>
    public class ResultErrorBindWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        [Fact]
        public async Task ResultErrorBindOkBadTaskAsync_Ok()
        {
            var initialResult = RUnitFactory.Some();
            var addingResult = RUnitFactory.Some();

            var result = await initialResult.ToTask().
                               ResultErrorBindOkBadTaskAsync(() => addingResult,
                                                             _ => RUnitFactory.None(CreateErrorTest()));

            Assert.True(result.Success);
        }

        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        [Fact]
        public async Task ResultErrorBindOkBadTaskAsync_Error()
        {
            var initialResult = RUnitFactory.None(CreateErrorListTwoTest());
            var addingResult = RUnitFactory.Some();
            var addingResultBad = RUnitFactory.None(CreateErrorTest());

            var result = await initialResult.ToTask().
                               ResultErrorBindOkBadTaskAsync(() => addingResult,
                                                             _ => addingResultBad);

            Assert.True(result.Failure);
            Assert.Equal(addingResultBad.GetErrors().Count, result.GetErrors().Count);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkTaskAsync_Ok_NoError()
        {
            var initialResult = RUnitFactory.SomeTask();
            var addingResult = RUnitFactory.Some();

            var result = await initialResult.ResultErrorBindOkTaskAsync(() => addingResult);

            Assert.True(result.Success);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkTaskAsync_Ok_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = RUnitFactory.SomeTask();
            var addingResult = RUnitFactory.None(initialError);

            var result = await initialResult.ResultErrorBindOkTaskAsync(() => addingResult);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(initialError));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkTaskAsync_Bad_NoError()
        {
            var initialError = CreateErrorTest();
            var initialResult = RUnitFactory.NoneTask(initialError);
            var addingResult = RUnitFactory.Some();

            var result = await initialResult.ResultErrorBindOkTaskAsync(() => addingResult);

            Assert.True(result.Failure);
            Assert.True(result.Equals(initialResult.Result));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkTaskAsync_Bad_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = RUnitFactory.NoneTask(initialError);
            var addingResult = RUnitFactory.None(initialError);

            var result = await initialResult.ResultErrorBindOkTaskAsync(() => addingResult);

            Assert.True(result.Failure);
            Assert.Single(result.GetErrors());
            Assert.True(result.Equals(initialResult.Result));
        }
    }
}