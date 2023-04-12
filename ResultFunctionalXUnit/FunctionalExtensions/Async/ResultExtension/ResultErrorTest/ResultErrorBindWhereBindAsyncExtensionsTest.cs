using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Values;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Обработка условий для асинхронного результирующего связывающего ответа для задачи-объекта. Тест
    /// </summary>
    public class ResultErrorBindWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        [Fact]
        public async Task ResultErrorBindOkBadBindAsync_Ok()
        {
            var initialResult = RUnitFactory.Some();
            var addingResult = RUnitFactory.Some();

            var result = await RUnitFactory.NoneTask(initialResult).
                               ResultErrorBindOkBadBindAsync(() => addingResult.ToTask(),
                                                            _ => RUnitFactory.NoneTask(CreateErrorTest()));

            Assert.True(result.Success);
        }

        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        [Fact]
        public async Task ResultErrorBindOkBadBindAsync_Error()
        {
            var initialResult = RUnitFactory.None(CreateErrorListTwoTest());
            var addingResult = RUnitFactory.Some();
            var addingResultBad = RUnitFactory.None(CreateErrorTest());

            var result = await RUnitFactory.NoneTask(initialResult).
                               ResultErrorBindOkBadBindAsync(() => addingResult.ToTask(),
                                                            _ => addingResultBad.ToTask());

            Assert.True(result.Failure);
            Assert.Equal(addingResultBad.GetErrors().Count, result.GetErrors().Count);
        }


        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkBindAsync_Ok_NoError()
        {
            var initialResult = RUnitFactory.SomeTask();
            var addingResult = RUnitFactory.SomeTask();

            var result = await initialResult.ResultErrorBindOkBindAsync(() => addingResult);

            Assert.True(result.Success);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkBindAsync_Ok_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = RUnitFactory.SomeTask();
            var addingResult = RUnitFactory.NoneTask(initialError);

            var result = await initialResult.ResultErrorBindOkBindAsync(() => addingResult);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(initialError));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkBindAsync_Bad_NoError()
        {
            var initialError = CreateErrorTest();
            var initialResult = RUnitFactory.NoneTask(initialError);
            var addingResult = RUnitFactory.SomeTask();

            var result = await initialResult.ResultErrorBindOkBindAsync(() => addingResult);

            Assert.True(result.Failure);
            Assert.True(result.Equals(initialResult.Result));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkBindAsync_Bad_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = RUnitFactory.NoneTask(initialError);
            var addingResult = RUnitFactory.NoneTask(initialError);

            var result = await initialResult.ResultErrorBindOkBindAsync(() => addingResult);

            Assert.True(result.Failure);
            Assert.Single(result.GetErrors());
            Assert.True(result.Equals(initialResult.Result));
        }
    }
}