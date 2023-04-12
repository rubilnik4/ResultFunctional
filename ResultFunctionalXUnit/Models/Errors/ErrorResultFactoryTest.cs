using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.AuthorizeErrors;
using ResultFunctional.Models.Errors.CommonErrors;
using ResultFunctional.Models.Errors.ConversionErrors;
using ResultFunctional.Models.Errors.DatabaseErrors;
using ResultFunctional.Models.Errors.RestErrors;
using ResultFunctional.Models.Factories;
using Xunit;

namespace ResultFunctionalXUnit.Models.Errors
{
    /// <summary>
    /// Фабрика создания типов ошибок. Тесты
    /// </summary>
    public class RErrorFactoryTest
    {
        /// <summary>
        /// Простая ошибка
        /// </summary>
        [Fact]
        public void SimpleError()
        {
            var errorResult = RErrorFactory.Simple("Ошибка");

            Assert.IsType<RSimpleError>(errorResult);
            Assert.IsType<RSimpleError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка с неопределенным типом
        /// </summary>
        [Fact]
        public void ErrorType()
        {
            var errorResult = RErrorFactory.ByType(CommonErrorType.Unknown, "Ошибка");

            Assert.IsType<RTypeError<CommonErrorType>>(errorResult);
            Assert.IsType<RTypeError<CommonErrorType>>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Общая ошибка
        /// </summary>
        [Fact]
        public void CommonError()
        {
            var errorResult = RErrorFactory.Common(CommonErrorType.Unknown, "Ошибка");

            Assert.IsType<RCommonError>(errorResult);
            Assert.IsType<RCommonError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка не найденного значения
        /// </summary>
        [Fact]
        public void ValueNotFoundError()
        {
            var errorResult = RErrorFactory.ValueNotFound<string>(String.Empty);

            Assert.IsAssignableFrom<IRValueNotFoundError>(errorResult);
            Assert.IsAssignableFrom<IRValueNotFoundError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка неверного значения
        /// </summary>
        [Fact]
        public void ValueNotValidError()
        {
            var errorResult = RErrorFactory.ValueNotValid(String.Empty, "Ошибка");

            Assert.IsAssignableFrom<IRValueNotValidError>(errorResult);
            Assert.IsAssignableFrom<IRValueNotValidError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка дублирующего значения
        /// </summary>
        [Fact]
        public void ValueDuplicatedError()
        {
            var errorResult = RErrorFactory.ValueDuplicate(String.Empty, "Ошибка");

            Assert.IsAssignableFrom<IRValueDuplicateError>(errorResult);
            Assert.IsAssignableFrom<IRValueDuplicateError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка авторизации
        /// </summary>
        [Fact]
        public void AuthorizeError()
        {
            var errorResult = RErrorFactory.Authorize(AuthorizeErrorType.Password, "Ошибка");

            Assert.IsType<RAuthorizeError>(errorResult);
            Assert.IsType<RAuthorizeError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseAccessError()
        {
            var errorResult = RErrorFactory.DatabaseAccess("Table", "Ошибка");

            Assert.IsType<RDatabaseAccessError>(errorResult);
            Assert.IsType<RDatabaseAccessError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseConnectionError()
        {
            var errorResult = RErrorFactory.DatabaseConnection("Table", "Ошибка");

            Assert.IsType<RDatabaseConnectionError>(errorResult);
            Assert.IsType<RDatabaseConnectionError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseValueNotValidError()
        {
            var errorResult = RErrorFactory.DatabaseValueNotValid(String.Empty, "Table", "Ошибка");

            Assert.IsAssignableFrom<IRDatabaseValueNotValidError>(errorResult);
            Assert.IsAssignableFrom<IRDatabaseValueNotValidError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseValueNotFoundError()
        {
            var errorResult = RErrorFactory.DatabaseValueNotFound(String.Empty, "Table", "Ошибка");

            Assert.IsAssignableFrom<IRDatabaseValueNotFoundError>(errorResult);
            Assert.IsAssignableFrom<IRDatabaseValueNotFoundError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseValueDuplicateError()
        {
            var errorResult = RErrorFactory.DatabaseValueDuplicate(String.Empty, "Table", "Ошибка");

            Assert.IsAssignableFrom<IRDatabaseValueDuplicatedError>(errorResult);
            Assert.IsAssignableFrom<IRDatabaseValueDuplicatedError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка rest сервиса
        /// </summary>
        [Fact]
        public void RestHostError()
        {
            var errorResult = RErrorFactory.RestHost(RestErrorType.BadRequest, "host", "Ошибка");

            Assert.IsType<RRestHostError>(errorResult);
            Assert.IsType<RRestHostError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка времени ожидания rest сервиса
        /// </summary>
        [Fact]
        public void RestTimeoutError()
        {
            var errorResult = RErrorFactory.RestTimeout("host", TimeSpan.FromSeconds(5), "Ошибка");

            Assert.IsType<RRestTimeoutError>(errorResult);
            Assert.IsType<RRestTimeoutError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка rest сервиса
        /// </summary>
        [Fact]
        public void RestMessageError()
        {
            var errorResult = RErrorFactory.Rest(RestErrorType.BadRequest, String.Empty, "Ошибка");

            Assert.IsType<RRestMessageError>(errorResult);
            Assert.IsType<RRestMessageError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка сериализации
        /// </summary>
        [Fact]
        public void SerializeError()
        {
            var errorResult = RErrorFactory.Serialize(String.Empty, "Ошибка");

            Assert.IsAssignableFrom<IRSerializeError>(errorResult);
            Assert.IsAssignableFrom<IRSerializeError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка десериализации
        /// </summary>
        [Fact]
        public void DeserializeError()
        {
            var errorResult = RErrorFactory.Deserialize<string>(String.Empty, "Ошибка");

            Assert.IsAssignableFrom<IRDeserializeError>(errorResult);
            Assert.IsAssignableFrom<IRDeserializeError>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка схемы
        /// </summary>
        [Fact]
        public void JsonSchemeError()
        {
            var errorResult = RErrorFactory.JsonScheme("scheme", "Ошибка");

            Assert.IsAssignableFrom<RJsonSchemeError>(errorResult);
            Assert.IsAssignableFrom<RJsonSchemeError>(errorResult.AppendException(new Exception()));
        }
    }
}