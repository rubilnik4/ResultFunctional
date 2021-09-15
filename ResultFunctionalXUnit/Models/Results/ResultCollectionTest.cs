using System.Collections.Generic;
using System.Linq;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Errors.Base;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.Models.Results
{
    /// <summary>
    /// Базовый вариант ответа с коллекцией. Тест
    /// </summary>
    public class ResultCollectionTest
    {
        /// <summary>
        /// Инициализация с ошибкой
        /// </summary>
        [Fact]
        public void Initialize_OneError()
        {
            var resultCollection = new ResultCollection<string>(CreateErrorTest());

            Assert.False(resultCollection.OkStatus);
            Assert.True(resultCollection.HasErrors);
            Assert.Single(resultCollection.Errors);
        }

        /// <summary>
        /// Инициализация со списком ошибок
        /// </summary>
        [Fact]
        public void Initialize_Errors()
        {
            var errors = CreateErrorListTwoTest();
            var resultCollection = new ResultCollection<string>(errors);

            Assert.False(resultCollection.OkStatus);
            Assert.True(resultCollection.HasErrors);
            Assert.Equal(errors.Count, resultCollection.Errors.Count);
        }

        /// <summary>
        /// Инициализация со значением
        /// </summary>
        [Fact]
        public void Initialize_HasValue()
        {
            var collection = GetRangeNumber();
            var value = new ResultCollection<int>(collection);

            Assert.True(value.OkStatus);
            Assert.False(value.HasErrors);
            Assert.Empty(value.Errors);
            Assert.True(value.Value.SequenceEqual(collection));
        }

        /// <summary>
        /// Добавление ошибки
        /// </summary>
        [Fact]
        public void AppendError()
        {
            var resultCollectionInitial = new ResultCollection<int>(GetRangeNumber());
            var errorToConcat = CreateErrorTest();

            var resultCollectionConcat = resultCollectionInitial.AppendError(errorToConcat);

            Assert.True(resultCollectionConcat.HasErrors);
            Assert.Single(resultCollectionConcat.Errors);
            Assert.True(errorToConcat.Equals(resultCollectionConcat.Errors.Last()));
            Assert.Equal(0, resultCollectionConcat.Value.Count);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalOne()
        {
            var resultCollectionInitial = new ResultCollection<int>(GetRangeNumber());
            var errorToConcat = CreateErrorTest();

            var resultCollectionConcat = resultCollectionInitial.ConcatErrors(errorToConcat);

            Assert.True(resultCollectionConcat.HasErrors);
            Assert.Single(resultCollectionConcat.Errors);
            Assert.True(errorToConcat.Equals(resultCollectionConcat.Errors.Last()));
            Assert.Equal(0, resultCollectionConcat.Value.Count);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var initialError = CreateErrorTest();
            var resultCollectionInitial = new ResultCollection<int>(initialError);
            var errorToConcat = CreateErrorTest();

            var resultCollectionConcat = resultCollectionInitial.ConcatErrors(errorToConcat);

            Assert.True(resultCollectionConcat.HasErrors);
            Assert.Equal(2, resultCollectionConcat.Errors.Count);
            Assert.True(initialError.Equals(resultCollectionConcat.Errors.First()));
            Assert.True(errorToConcat.Equals(resultCollectionConcat.Errors.Last()));
            Assert.Equal(0, resultCollectionConcat.Value.Count);
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта без ошибок при отправке пустого списка
        /// </summary>
        [Fact]
        public void ConcatErrors_OkStatus_EmptyList()
        {
            var collection = GetRangeNumber();
            var resultCollectionInitial = new ResultCollection<int>(collection);
            var errorsToConcat = Enumerable.Empty<IErrorResult>();

            var resultValueConcat = resultCollectionInitial.ConcatErrors(errorsToConcat);

            Assert.True(resultValueConcat.OkStatus);
            Assert.True(collection.SequenceEqual(resultValueConcat.Value));
        }
    }
}