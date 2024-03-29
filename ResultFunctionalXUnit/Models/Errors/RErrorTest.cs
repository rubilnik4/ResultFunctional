﻿using System;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;
using ResultFunctional.Models.Errors.CommonErrors;
using ResultFunctional.Models.Errors.DatabaseErrors;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.Models.Errors
{
    /// <summary>
    /// Ошибка результирующего ответа. Тесты
    /// </summary>
    public class RErrorTest
    {
        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        [Fact]
        public void HasErrorTypeCommon()
        {
            var errorResult = (IRError)RErrorFactory.Common(CommonErrorType.Unknown, "Неизвестная ошибка");

            bool hasType = errorResult.HasErrorType<CommonErrorType>();

            Assert.True(hasType);
        }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        [Fact]
        public void HasNotErrorTypeCommon()
        {
            var errorResult = (IRError)RErrorFactory.Common(CommonErrorType.Unknown, "Неизвестная ошибка");

            bool hasType = errorResult.HasErrorType<DatabaseErrorType>();

            Assert.False(hasType);
        }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        [Fact]
        public void HasNotErrorTypeDatabase()
        {
            var errorResult = (IRError)RErrorFactory.Simple("Неизвестная ошибка");

            bool hasType = errorResult.HasErrorType<DatabaseErrorType>();

            Assert.False(hasType);
        }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        [Fact]
        public void HasErrorTypeCommon_CommonType()
        {
            var errorResult = (IRError)RErrorFactory.Common(CommonErrorType.Unknown, "Неизвестная ошибка");

            bool hasType = errorResult.HasErrorType(CommonErrorType.Unknown);

            Assert.True(hasType);
        }


        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        [Fact]
        public void HasNotErrorTypeCommon_CommonType()
        {
            var errorResult = (IRError)RErrorFactory.Common(CommonErrorType.Unknown, "Неизвестная ошибка");

            bool hasType = errorResult.HasErrorType(CommonErrorType.ValueDuplicated);

            Assert.False(hasType);
        }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        [Fact]
        public void HasNotErrorTypeDatabase_CommonType()
        {
            var errorResult = (IRError)RErrorFactory.Simple("Неизвестная ошибка");

            bool hasType = errorResult.HasErrorType(DatabaseErrorType.Connection);

            Assert.False(hasType);
        }

        /// <summary>
        /// Преобразование в строку
        /// </summary>
        [Fact]
        public void ToStringCommon()
        {
            var errorResult = RErrorFactory.Common(CommonErrorType.Unknown, "Неизвестная ошибка");

            var error = errorResult.ToString();

            Assert.Equal(CommonErrorType.Unknown.ToString(), error);
        }

        /// <summary>
        /// Вернуть ответ с одной ошибкой
        /// </summary>
        [Fact]
        public void ToResult_HasOneError()
        {
            var error = CreateErrorTest();

            var rMaybe = error.ToRUnit();

            Assert.True(rMaybe.Failure);
            Assert.Equal(1, rMaybe.GetErrors().Count);
        }

        /// <summary>
        /// Вернуть результирующий ответ с одной ошибкой
        /// </summary>
        [Fact]
        public void ToRValue_HasStringType_HasOneError()
        {
            var error = CreateErrorTest();

            var rValue = error.ToRValue<string>();

            Assert.True(rValue.Failure);
            Assert.Equal(1, rValue.GetErrors().Count);
        }

        /// <summary>
        /// Вернуть результирующий ответ с одной ошибкой
        /// </summary>
        [Fact]
        public void ToRList_HasStringType_HasOneError()
        {
            var error = CreateErrorTest();

            var rList = error.ToRList<string>();

            Assert.True(rList.Failure);
            Assert.Equal(1, rList.GetErrors().Count);
        }

        /// <summary>
        /// Преобразование в множество с одним элементом
        /// </summary>
        [Fact]
        public void IsEnumerableType_HasOne()
        {
            var error = CreateErrorTest();

            Assert.Single(error);
        }

        /// <summary>
        /// Преобразование в строку выводит тип ошибки
        /// </summary>
        [Fact]
        public void ToStringFormatIsErrorType()
        {
            var error = CreateErrorTest();

            Assert.IsType<RCommonError>(error);
            Assert.Equal(((RCommonError)error).ErrorType.ToString(), error.ToString());
        }

        /// <summary>
        /// Проверка добавления ошибки
        /// </summary>
        [Fact]
        public void AppendException()
        {
            var error = CreateErrorTest();
            var exception = new ArgumentException();

            var errorWithException = error.AppendException(exception);

            Assert.True(exception.Equals(errorWithException.Exception));
        }

        /// <summary>
        /// Наличие специфической ошибки
        /// </summary>
        [Fact]
        public void HasError()
        {
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");

            Assert.True(databaseTableError.HasError<IRDatabaseAccessError>());
            Assert.True(databaseTableError.HasError<RDatabaseAccessError>());
        }

        /// <summary>
        /// Наличие специфической ошибки
        /// </summary>
        [Fact]
        public void IsError()
        {
            var databaseTableError = RErrorFactory.DatabaseAccess("TestTable", "error");

            Assert.False(databaseTableError.IsError<IRDatabaseAccessError>());
            Assert.True(databaseTableError.IsError<RDatabaseAccessError>());
        }

        /// <summary>
        /// Параметры ошибки
        /// </summary>
        [Fact]
        public void ErrorDescription()
        {
            const string description = "Неизвестная ошибка";
            var errorResult = (IRError)RErrorFactory.Simple(description);

            Assert.Equal(errorResult.Description, description);
            Assert.Equal(errorResult.Id, errorResult.GetType().Name);
        }
    }
}