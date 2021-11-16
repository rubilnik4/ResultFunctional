using System;
using System.Net.Http;

using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Errors.CommonErrors;
using ResultFunctional.Models.Implementations.Errors.ConversionErrors;
using ResultFunctional.Models.Implementations.Errors.DatabaseErrors;
using ResultFunctional.Models.Implementations.Errors.RestErrors;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Errors.conversionErrors;
using ResultFunctional.Models.Interfaces.Errors.ConversionErrors;
using ResultFunctional.Models.Interfaces.Errors.DatabaseErrors;
using Xunit;

namespace ResultFunctionalXUnit.Models.Errors
{
    /// <summary>
    /// Фабрика создания типов ошибок. Тесты
    /// </summary>
    public class ErrorResultFactoryTest
    {
        /// <summary>
        /// Простая ошибка
        /// </summary>
        [Fact]
        public void SimpleError()
        {
            var errorResult = ErrorResultFactory.SimpleErrorType("Ошибка");

            Assert.IsType<SimpleErrorResult>(errorResult);
            Assert.IsType<SimpleErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка с неопределенным типом
        /// </summary>
        [Fact]
        public void ErrorType()
        {
            var errorResult = ErrorResultFactory.ErrorType(CommonErrorType.Unknown, "Ошибка");

            Assert.IsType<ErrorTypeResult<CommonErrorType>>(errorResult);
            Assert.IsType<ErrorTypeResult<CommonErrorType>>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Общая ошибка
        /// </summary>
        [Fact]
        public void CommonError()
        {
            var errorResult = ErrorResultFactory.CommonError(CommonErrorType.Unknown, "Ошибка");

            Assert.IsType<CommonErrorResult>(errorResult);
            Assert.IsType<CommonErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка не найденного значения
        /// </summary>
        [Fact]
        public void ValueNotFoundError()
        {
            var errorResult = ErrorResultFactory.ValueNotFoundError(String.Empty, typeof(string));

            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(errorResult);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка неверного значения
        /// </summary>
        [Fact]
        public void ValueNotValidError()
        {
            var errorResult = ErrorResultFactory.ValueNotValidError(String.Empty, typeof(string), "Ошибка");

            Assert.IsAssignableFrom<IValueNotValidErrorResult>(errorResult);
            Assert.IsAssignableFrom<IValueNotValidErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка дублирующего значения
        /// </summary>
        [Fact]
        public void ValueDuplicatedError()
        {
            var errorResult = ErrorResultFactory.ValueDuplicateError(String.Empty, typeof(string), "Ошибка");

            Assert.IsAssignableFrom<IValueDuplicateErrorResult>(errorResult);
            Assert.IsAssignableFrom<IValueDuplicateErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка авторизации
        /// </summary>
        [Fact]
        public void AuthorizeError()
        {
            var errorResult = ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Password, "Ошибка");

            Assert.IsType<AuthorizeErrorResult>(errorResult);
            Assert.IsType<AuthorizeErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseAccessError()
        {
            var errorResult = ErrorResultFactory.DatabaseAccessError("Table", "Ошибка");

            Assert.IsType<DatabaseAccessErrorResult>(errorResult);
            Assert.IsType<DatabaseAccessErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseConnectionError()
        {
            var errorResult = ErrorResultFactory.DatabaseConnectionError("Table", "Ошибка");

            Assert.IsType<DatabaseConnectionErrorResult>(errorResult);
            Assert.IsType<DatabaseConnectionErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseValueNotValidError()
        {
            var errorResult = ErrorResultFactory.DatabaseValueNotValidError(String.Empty, "Table", "Ошибка");

            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(errorResult);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseValueNotFoundError()
        {
            var errorResult = ErrorResultFactory.DatabaseValueNotFoundError(String.Empty, "Table", "Ошибка");

            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(errorResult);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseValueDuplicateError()
        {
            var errorResult = ErrorResultFactory.DatabaseValueDuplicateError(String.Empty, "Table", "Ошибка");

            Assert.IsAssignableFrom<IDatabaseValueDuplicatedErrorResult>(errorResult);
            Assert.IsAssignableFrom<IDatabaseValueDuplicatedErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка rest сервиса
        /// </summary>
        [Fact]
        public void RestHostError()
        {
            var errorResult = ErrorResultFactory.RestHostError(RestErrorType.BadRequest, "host", "Ошибка");

            Assert.IsType<RestHostErrorResult>(errorResult);
            Assert.IsType<RestHostErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка времени ожидания rest сервиса
        /// </summary>
        [Fact]
        public void RestTimeoutError()
        {
            var errorResult = ErrorResultFactory.RestTimeoutError("host", TimeSpan.FromSeconds(5), "Ошибка");

            Assert.IsType<RestTimeoutErrorResult>(errorResult);
            Assert.IsType<RestTimeoutErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка rest сервиса
        /// </summary>
        [Fact]
        public void RestMessageError()
        {
            var errorResult = ErrorResultFactory.RestError(RestErrorType.BadRequest, new HttpResponseMessage(), "Ошибка");

            Assert.IsType<RestMessageErrorResult>(errorResult);
            Assert.IsType<RestMessageErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка сериализации
        /// </summary>
        [Fact]
        public void SerializeError()
        {
            var errorResult = ErrorResultFactory.SerializeError(String.Empty, "Ошибка");

            Assert.IsAssignableFrom<ISerializeErrorResult>(errorResult);
            Assert.IsAssignableFrom<ISerializeErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка десериализации
        /// </summary>
        [Fact]
        public void DeserializeError()
        {
            var errorResult = ErrorResultFactory.DeserializeError<string>(String.Empty, "Ошибка");

            Assert.IsAssignableFrom<IDeserializeErrorResult>(errorResult);
            Assert.IsAssignableFrom<IDeserializeErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка схемы
        /// </summary>
        [Fact]
        public void JsonSchemeError()
        {
            var errorResult = ErrorResultFactory.JsonSchemeError("scheme", "Ошибка");

            Assert.IsAssignableFrom<JsonSchemeErrorResult>(errorResult);
            Assert.IsAssignableFrom<JsonSchemeErrorResult>(errorResult.AppendException(new Exception()));
        }
    }
}