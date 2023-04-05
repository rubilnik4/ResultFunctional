using System.Linq;
using ResultFunctional.Models.Implementations.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultErrorTest
{
    /// <summary>
    /// Обработка условий для результирующего связывающего ответа. Тест
    /// </summary>
    public class ResultErrorBindWhereExtensionsTest
    {
        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        [Fact]
        public void ResultErrorBindOkBad_Ok()
        {
            var initialResult = new ResultError();
            var addingResult = new ResultError();

            var result = initialResult.ResultErrorBindOkBad(() => addingResult,
                                                            _ => new ResultError(CreateErrorTest()));

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Выполнение положительного или негативного условия результирующего ответа со связыванием или возвращение предыдущей ошибки в результирующем ответе
        /// </summary>   
        [Fact]
        public void ResultErrorBindOkBad_Error()
        {
            var initialResult = new ResultError(CreateErrorListTwoTest());
            var addingResult = new ResultError();
            var addingResultBad = new ResultError(CreateErrorTest());

            var result = initialResult.ResultErrorBindOkBad(() => addingResult,
                                                            _ => addingResultBad);

            Assert.True(result.HasErrors);
            Assert.Equal(addingResultBad.Errors.Count, result.Errors.Count);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public void ResultErrorBindOk_Ok_NoError()
        {
            var initialResult = new ResultError();
            var addingResult = new ResultError();

            var result = initialResult.ResultErrorBindOk(() => addingResult);

            Assert.True(result.OkStatus);
        }

        /// <summary>
        /// Результирующий ответ без ошибок и добавление объекта с ошибкой
        /// </summary>
        [Fact]
        public void ResultErrorBindOk_Ok_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = new ResultError();
            var addingResult = new ResultError(initialError);

            var result = initialResult.ResultErrorBindOk(() => addingResult);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public void ResultErrorBindOk_Bad_NoError()
        {
            var initialError = CreateErrorTest();
            var initialResult = new ResultError(initialError);
            var addingResult = new ResultError();

            var result = initialResult.ResultErrorBindOk(() => addingResult);

            Assert.True(result.HasErrors);
            Assert.True(result.Equals(initialResult));
        }

        /// <summary>
        /// Результирующий ответ с ошибкой и добавление объекта без ошибки
        /// </summary>
        [Fact]
        public void ResultErrorBindOk_Bad_HasError()
        {
            var initialError = CreateErrorTest();
            var initialResult = new ResultError(initialError);
            var addingResult = new ResultError(initialError);

            var result = initialResult.ResultErrorBindOk(() => addingResult);

            Assert.True(result.HasErrors);
            Assert.Single(result.Errors);
            Assert.True(result.Equals(initialResult));
        }
    }
}