using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.Lists;
using ResultFunctional.Models.Errors.BaseErrors;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Data.Collections;

namespace ResultFunctionalXUnit.Models.Options
{
    /// <summary>
    /// Базовый вариант ответа с коллекцией. Тест
    /// </summary>
    public class RListTest
    {
        /// <summary>
        /// Инициализация с ошибкой
        /// </summary>
        [Fact]
        public void Initialize_OneError()
        {
            var resultCollection = CreateErrorTest().ToRList<int>();

            Assert.False(resultCollection.Success);
            Assert.True(resultCollection.Failure);
            Assert.Single(resultCollection.GetErrors());
        }

        /// <summary>
        /// Инициализация со списком ошибок
        /// </summary>
        [Fact]
        public void Initialize_Errors()
        {
            var errors = CreateErrorListTwoTest();
            var resultCollection = errors.ToRList<int>();

            Assert.False(resultCollection.Success);
            Assert.True(resultCollection.Failure);
            Assert.Equal(errors.Count, resultCollection.GetErrors().Count);
        }

        /// <summary>
        /// Инициализация со значением
        /// </summary>
        [Fact]
        public void Initialize_HasValue()
        {
            var collection = GetRangeNumber();
            var value = collection.ToRList();

            Assert.True(value.Success);
            Assert.False(value.Failure);
            Assert.Empty(value.GetErrorsOrEmpty());
            Assert.True(value.GetValue().SequenceEqual(collection));
        }

        /// <summary>
        /// Добавление ошибки
        /// </summary>
        [Fact]
        public void AppendError()
        {
            var resultCollectionInitial = GetRangeNumber().ToRList();
            var errorToConcat = CreateErrorTest();

            var resultCollectionConcat = resultCollectionInitial.AppendError(errorToConcat);

            Assert.True(resultCollectionConcat.Failure);
            Assert.Single(resultCollectionConcat.GetErrors());
            Assert.True(errorToConcat.Equals(resultCollectionConcat.GetErrors().Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalOne()
        {
            var resultCollectionInitial = GetRangeNumber().ToRList();
            var errorToConcat = CreateErrorTest();

            var resultCollectionConcat = resultCollectionInitial.ConcatErrors(errorToConcat);

            Assert.True(resultCollectionConcat.Failure);
            Assert.Single(resultCollectionConcat.GetErrors());
            Assert.True(errorToConcat.Equals(resultCollectionConcat.GetErrors().Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var initialError = CreateErrorTest();
            var resultCollectionInitial = initialError.ToRList<int>();
            var errorToConcat = CreateErrorTest();

            var resultCollectionConcat = resultCollectionInitial.ConcatErrors(errorToConcat);

            Assert.True(resultCollectionConcat.Failure);
            Assert.Equal(2, resultCollectionConcat.GetErrors().Count);
            Assert.True(initialError.Equals(resultCollectionConcat.GetErrors().First()));
            Assert.True(errorToConcat.Equals(resultCollectionConcat.GetErrors().Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта без ошибок при отправке пустого списка
        /// </summary>
        [Fact]
        public void ConcatErrors_OkStatus_EmptyList()
        {
            var collection = GetRangeNumber();
            var resultCollectionInitial = collection.ToRList();
            var errorsToConcat = Enumerable.Empty<IRError>();

            var resultValueConcat = resultCollectionInitial.ConcatErrors(errorsToConcat);

            Assert.True(resultValueConcat.Success);
            Assert.True(collection.SequenceEqual(resultValueConcat.GetValue()));
        }
        
        /// <summary>
        /// Преобразование в значение
        /// </summary>
        [Fact]
        public void ToRValue_Ok()
        {
            var collection = GetRangeNumber();
            var resultCollectionInitial = collection.ToRList();

            var rValue = resultCollectionInitial.ToRValue();

            Assert.True(rValue.Success);
            Assert.True(rValue.GetValue().Equals(resultCollectionInitial.GetValue()));
        }

        /// <summary>
        /// Преобразование в значение
        /// </summary>
        [Fact]
        public void ToRValue_Error()
        {
            var initialError = CreateErrorTest();
            var resultCollectionInitial = initialError.ToRList<int>();

            var rValue = resultCollectionInitial.ToRValue();

            Assert.True(rValue.Failure);
            Assert.Single(rValue.GetErrors());
            Assert.True(initialError.Equals(rValue.GetErrors().Last()));
        }
    }
}