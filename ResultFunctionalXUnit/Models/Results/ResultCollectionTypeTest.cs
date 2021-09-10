using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.Models.Results
{
    /// <summary>
    /// Базовый вариант ответа с коллекцией и типом ошибки. Тесты
    /// </summary>
    public class ResultCollectionTypeTest
    {
        /// <summary>
        /// Инициализация одной ошибкой
        /// </summary>
        [Fact]
        public void Initialize()
        {
            var valueError = ErrorResultFactory.ValueNotFoundError("test", GetType());
            var resultError = new ResultCollectionType<int, IValueNotFoundErrorResult>(valueError);

            Assert.True(resultError.HasErrors);
            Assert.Single(resultError.Errors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(resultError.Errors.First());
        }

        /// <summary>
        /// Инициализация базовой ошибкой
        /// </summary>
        [Fact]
        public void InitializeBase()
        {
            var valueNotValidError = ErrorResultFactory.ValueNotValidError("test", GetType(), "ValueNotFound");
            var resultError = new ResultCollectionType<int, IValueNotFoundErrorResult>(valueNotValidError);

            Assert.True(resultError.HasErrors);
            Assert.Single(resultError.Errors);
            Assert.Equal(0, resultError.ErrorsByType.Count);
            Assert.IsAssignableFrom<IValueNotValidErrorResult>(resultError.Errors.Last());
        }

        /// <summary>
        /// Добавить ошибку
        /// </summary>
        [Fact]
        public void AppendError()
        {
            var collection = GetRangeNumber();
            var valueError = ErrorResultFactory.ValueNotFoundError("test", GetType());
            var resultError = new ResultCollectionType<int, IValueNotFoundErrorResult>(collection);

            var resultAppend = resultError.AppendError(valueError);

            Assert.True(resultAppend.HasErrors);
            Assert.Single(resultAppend.Errors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(resultAppend.Errors.Last());
            Assert.IsAssignableFrom<IResultCollectionType<int, IValueNotFoundErrorResult>>(resultAppend);
        }

        /// <summary>
        /// Добавить ошибку
        /// </summary>
        [Fact]
        public void AppendErrorBase()
        {
            var valueNotFoundError = ErrorResultFactory.ValueNotFoundError("test", GetType());
            var valueNotValidError = ErrorResultFactory.ValueNotValidError("test", GetType(), "ValueNotFound");
            var resultError = new ResultCollectionType<int, IValueNotFoundErrorResult>(valueNotValidError);

            var resultAppend = resultError.AppendError(valueNotFoundError);

            Assert.True(resultAppend.HasErrors);
            Assert.Equal(2, resultAppend.Errors.Count);
            Assert.Equal(1, resultAppend.ErrorsByType.Count);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(resultAppend.Errors.Last());
            Assert.IsAssignableFrom<IResultCollectionType<int, IValueNotFoundErrorResult>>(resultAppend);
        }

        /// <summary>
        /// Добавить ошибки
        /// </summary>
        [Fact]
        public void ConcatErrors()
        {
            var collection = GetRangeNumber();
            var valueError = ErrorResultFactory.ValueNotFoundError("test", GetType());
            var resultError = new ResultCollectionType<int, IValueNotFoundErrorResult>(collection);

            var resultAppend = resultError.ConcatErrors(new List<IValueNotFoundErrorResult> { valueError });

            Assert.True(resultAppend.HasErrors);
            Assert.Single(resultAppend.Errors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(resultAppend.Errors.Last());
            Assert.IsAssignableFrom<IResultCollectionType<int, IValueNotFoundErrorResult>>(resultAppend);
        }

        /// <summary>
        /// Добавить ошибки
        /// </summary>
        [Fact]
        public void ConcatErrorsBase()
        {
            var valueNotFoundError = ErrorResultFactory.ValueNotFoundError("test", GetType());
            var valueNotValidError = ErrorResultFactory.ValueNotValidError("test", GetType(), "ValueNotFound");
            var resultError = new ResultCollectionType<int, IValueNotFoundErrorResult>(valueNotValidError);

            var resultAppend = resultError.ConcatErrors(new List<IValueNotFoundErrorResult> { valueNotFoundError });

            Assert.True(resultAppend.HasErrors);
            Assert.Equal(2, resultAppend.Errors.Count);
            Assert.Equal(1, resultAppend.ErrorsByType.Count);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(resultAppend.Errors.Last());
            Assert.IsAssignableFrom<IResultCollectionType<int, IValueNotFoundErrorResult>>(resultAppend);
        }
    }
}