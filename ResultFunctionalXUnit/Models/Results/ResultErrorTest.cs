﻿using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.DatabaseErrors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.Models.Results
{
    /// <summary>
    /// Базовый вариант ответа. Тест
    /// </summary>
    public class ResultErrorTest
    {
        /// <summary>
        /// Базовая инициализация. Отсутствие ошибок
        /// </summary>
        [Fact]
        public void Initialize_Base_OkStatus()
        {
            var resultError = new ResultError();

            Assert.True(resultError.OkStatus);
            Assert.False(resultError.HasErrors);
            Assert.Empty(resultError.Errors);
        }

        /// <summary>
        /// Инициализация одной ошибкой
        /// </summary>
        [Fact]
        public void Initialize_Error_HasOne()
        {
            var resultError = new ResultError(CreateErrorTest());

            Assert.False(resultError.OkStatus);
            Assert.True(resultError.HasErrors);
            Assert.Single(resultError.Errors);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalOne()
        {
            var resultErrorInitial = new ResultError();
            var errorToConcat = CreateErrorTest();

            var resultErrorConcat = resultErrorInitial.ConcatErrors(errorToConcat);

            Assert.True(resultErrorConcat.HasErrors);
            Assert.Equal(1, resultErrorConcat.Errors.Count);
            Assert.True(errorToConcat.Equals(resultErrorConcat.Errors.Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var resultErrorInitial = new ResultError(CreateErrorTest());
            var errorToConcat = CreateErrorTest();

            var resultErrorConcat = resultErrorInitial.ConcatErrors(errorToConcat);

            Assert.True(resultErrorConcat.HasErrors);
            Assert.Equal(2, resultErrorConcat.Errors.Count);
            Assert.True(errorToConcat.Equals(resultErrorConcat.Errors.Last()));
        }

        /// <summary>
        /// Наличие специфической ошибки
        /// </summary>
        [Fact]
        public void HasError()
        {
            var databaseTableError = ErrorResultFactory.DatabaseTableError("TestTable", "error");
            var result = databaseTableError.ToResult();

            Assert.False(result.HasError<DatabaseErrorResult>());
            Assert.True(result.HasError<DatabaseTableErrorResult>());
        }

        /// <summary>
        /// Наличие специфической ошибки
        /// </summary>
        [Fact]
        public void IncludeError()
        {
            var databaseTableError = ErrorResultFactory.DatabaseTableError("TestTable", "error");
            var result = databaseTableError.ToResult();

            Assert.True(result.FromError<DatabaseErrorResult>());
            Assert.True(result.FromError<DatabaseTableErrorResult>());
        }

        /// <summary>
        /// Получить тиы ошибок
        /// </summary>
        [Fact]
        public void GetErrorTypes()
        {
            var valueNotFoundError = ErrorResultFactory.ValueNotFoundError("value", GetType());
            var databaseTableError = ErrorResultFactory.DatabaseTableError("TestTable", "error");
            var errors = new List<IErrorResult> { valueNotFoundError, databaseTableError };
            var result = new ResultError(errors);

            var errorTypes = result.GetErrorTypes();

            Assert.True(errorTypes.SequenceEqual(errors.Select(error => error.GetType())));
        }

        /// <summary>
        /// Наличие типа ошибки
        /// </summary>
        [Fact]
        public void HasErrorType()
        {
            var valueNotFoundError = ErrorResultFactory.ValueNotFoundError("value", GetType());
            var databaseTableError = ErrorResultFactory.DatabaseTableError("TestTable", "error");
            var errors = new List<IErrorResult> { valueNotFoundError, databaseTableError };
            var result = new ResultError(errors);

            Assert.True(result.HasErrorType<DatabaseErrorType>());
        }

        /// <summary>
        /// Получить тип ошибки
        /// </summary>
        [Fact]
        public void GetErrorType()
        {
            var valueNotFoundError = ErrorResultFactory.ValueNotFoundError("value", GetType());
            var databaseTableError = ErrorResultFactory.DatabaseTableError("TestTable", "error");
            var errors = new List<IErrorResult> { valueNotFoundError, databaseTableError };
            var result = new ResultError(errors);

            var errorType = result.GetErrorType<DatabaseErrorType>();
            Assert.NotNull(errorType);
            Assert.Equal(DatabaseErrorType.TableAccess, errorType?.ErrorType);
        }

        /// <summary>
        /// Получить типы ошибок
        /// </summary>
        [Fact]
        public void GetErrorTypesT()
        {
            var valueNotFoundError = ErrorResultFactory.ValueNotFoundError("value", GetType());
            var databaseTableError = ErrorResultFactory.DatabaseTableError("TestTable", "error");
            var errors = new List<IErrorResult> { valueNotFoundError, databaseTableError };
            var result = new ResultError(errors);

            var errorTypes = result.GetErrorTypes<DatabaseErrorType>();
            Assert.Equal(1, errorTypes.Count);
            Assert.Equal(DatabaseErrorType.TableAccess, errorTypes.First().ErrorType);
        }
    }
}