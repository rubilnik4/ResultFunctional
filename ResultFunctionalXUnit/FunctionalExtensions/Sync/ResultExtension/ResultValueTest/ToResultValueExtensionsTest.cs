using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением. Тесты
    /// </summary>
    public class ToResultValueExtensionsTest
    {
        /// <summary>
        /// Проверить объект. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultValue_Ok()
        {
            const string initialString = "NotNull";

            var resultString = initialString.ToResultValue();

            Assert.True(resultString.OkStatus);
            Assert.Equal(initialString, resultString.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultValueNullValueCheck_Ok()
        {
            const string initialString = "NotNull";

            var resultString = initialString.ToResultValueNullValueCheck(CreateErrorTest());

            Assert.True(resultString.OkStatus);
            Assert.Equal(initialString, resultString.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultValueNullValueCheck_ErrorNull()
        {
            const string? initialString = null;
            var initialError = CreateErrorTest();
            var resultString = initialString!.ToResultValueNullValueCheck(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultValueNullCheck_Ok()
        {
            const string initialString = "NotNull";

            var result = initialString.ToResultValueNullCheck(CreateErrorTest());

            Assert.True(result.OkStatus);
            Assert.Equal(initialString, result.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultValueNullCheck_ErrorNull()
        {
            const string? initialString = null;
            var initialError = CreateErrorTest();
            var result = initialString.ToResultValueNullCheck(initialError);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultValueNullCheckStruct_Ok()
        {
            int? initialInt = 1;

            var result = initialInt.ToResultValueNullCheck(CreateErrorTest());

            Assert.True(result.OkStatus);
            Assert.Equal(initialInt, result.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultValueNullCheckStruct_ErrorNull()
        {
            int? initialInt = null;
            var initialError = CreateErrorTest();
            var result = initialInt.ToResultValueNullCheck(initialError);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError));
        }


        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public void ToResultValueBind_OkStatus()
        {
            var resultNoError = new ResultError();
            const string value = "OkStatus";
            var resultValue = new ResultValue<string>(value);

            var resultValueAfter = resultNoError.ToResultBindValue(resultValue);

            Assert.True(resultValueAfter.OkStatus);
            Assert.Equal(value, resultValueAfter.Value);
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultValueBind_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = new ResultError(error);
            const string value = "BadStatus";
            var resultValue = new ResultValue<string>(value);

            var resultValueAfter = resultHasError.ToResultBindValue(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public void ToResultValueFromResultError_OkStatus()
        {
            var resultNoError = new ResultError();
            const string value = "OkStatus";

            var resultValue = resultNoError.ToResultValue(value);

            Assert.True(resultValue.OkStatus);
            Assert.Equal(value, resultValue.Value);
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultValueFromResultError_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = new ResultError(error);
            const string value = "BadStatus";

            var resultValue = resultHasError.ToResultValue(value);

            Assert.True(resultValue.HasErrors);
            Assert.Single(resultValue.Errors);
            Assert.True(error.Equals(resultValue.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой в результирующем ответе
        /// </summary>      
        [Fact]
        public void ToResultValueBind_HasErrorsBind()
        {
            var resultNoError = new ResultError();
            var error = CreateErrorTest();
            var resultValue = new ResultValue<string>(error);

            var resultValueAfter = resultNoError.ToResultBindValue(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultValueBind_HasErrorsBindInitial()
        {
            var error = CreateErrorTest();
            var resultHasError = new ResultError(error);
            var errors = CreateErrorListTwoTest();
            var resultValue = new ResultValue<string>(errors);

            var resultValueAfter = resultHasError.ToResultBindValue(resultValue);

            Assert.True(resultValueAfter.HasErrors);
            Assert.Single(resultValueAfter.Errors);
            Assert.True(error.Equals(resultValueAfter.Errors.Last()));
        }
    }
}