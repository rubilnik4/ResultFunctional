using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest
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
            var initialResult = new ResultError();
            var addingResult = new ResultError();

            var result = await ResultErrorFactory.CreateTaskResultError(initialResult).
                               ResultErrorBindOkBadTaskAsync(() => addingResult,
                                                             _ => new ResultError(CreateErrorTest()));

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        [Fact]
        public async Task ResultErrorBindOkBadTaskAsync_Error()
        {
            var initialResult = new ResultError(CreateErrorListTwoTest());
            var addingResult = new ResultError();
            var addingResultBad = new ResultError(CreateErrorTest());

            var result = await ResultErrorFactory.CreateTaskResultError(initialResult).
                               ResultErrorBindOkBadTaskAsync(() => addingResult,
                                                             _ => addingResultBad);

            Assert.True(result.HasErrors);
            Assert.Equal(addingResultBad.Errors.Count, result.Errors.Count);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkTaskAsync_Ok_NoError()
        {
            var initialResult = ResultErrorFactory.CreateTaskResultError();
            var addingResult = new ResultError();

            var result = await initialResult.ResultErrorBindOkTaskAsync(() => addingResult);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkTaskAsync_Ok_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = ResultErrorFactory.CreateTaskResultError();
            var addingResult = new ResultError(initialError);

            var result = await initialResult.ResultErrorBindOkTaskAsync(() => addingResult);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkTaskAsync_Bad_NoError()
        {
            var initialError = CreateErrorTest();
            var initialResult = ResultErrorFactory.CreateTaskResultError(initialError);
            var addingResult = new ResultError();

            var result = await initialResult.ResultErrorBindOkTaskAsync(() => addingResult);

            Assert.True(result.HasErrors);
            Assert.True(result.Equals(initialResult.Result));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public async Task ResultErrorBindOkTaskAsync_Bad_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = ResultErrorFactory.CreateTaskResultError(initialError);
            var addingResult = new ResultError(initialError);

            var result = await initialResult.ResultErrorBindOkTaskAsync(() => addingResult);

            Assert.True(result.HasErrors);
            Assert.Single(result.Errors);
            Assert.True(result.Equals(initialResult.Result));
        }
    }
}