using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
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
            var rList = CreateErrorTest().ToRList<int>();

            Assert.False(rList.Success);
            Assert.True(rList.Failure);
            Assert.Single(rList.GetErrors());
        }

        /// <summary>
        /// Инициализация со списком ошибок
        /// </summary>
        [Fact]
        public void Initialize_Errors()
        {
            var errors = CreateErrorListTwoTest();
            var rList = errors.ToRList<int>();

            Assert.False(rList.Success);
            Assert.True(rList.Failure);
            Assert.Equal(errors.Count, rList.GetErrors().Count);
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
            var rListInitial = GetRangeNumber().ToRList();
            var errorToConcat = CreateErrorTest();

            var rListConcat = rListInitial.AppendError(errorToConcat);

            Assert.True(rListConcat.Failure);
            Assert.Single(rListConcat.GetErrors());
            Assert.True(errorToConcat.Equals(rListConcat.GetErrors().Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с одной ошибкой
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalOne()
        {
            var rListInitial = GetRangeNumber().ToRList();
            var errorToConcat = CreateErrorTest();

            var rListConcat = rListInitial.ConcatErrors(errorToConcat);

            Assert.True(rListConcat.Failure);
            Assert.Single(rListConcat.GetErrors());
            Assert.True(errorToConcat.Equals(rListConcat.GetErrors().Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта с двумя ошибками
        /// </summary>
        [Fact]
        public void ConcatErrors_TotalTwo()
        {
            var initialError = CreateErrorTest();
            var rListInitial = initialError.ToRList<int>();
            var errorToConcat = CreateErrorTest();

            var rListConcat = rListInitial.ConcatErrors(errorToConcat);

            Assert.True(rListConcat.Failure);
            Assert.Equal(2, rListConcat.GetErrors().Count);
            Assert.True(initialError.Equals(rListConcat.GetErrors().First()));
            Assert.True(errorToConcat.Equals(rListConcat.GetErrors().Last()));
        }

        /// <summary>
        /// Добавление ошибки и получение нового объекта без ошибок при отправке пустого списка
        /// </summary>
        [Fact]
        public void ConcatErrors_OkStatus_EmptyList()
        {
            var collection = GetRangeNumber();
            var rListInitial = collection.ToRList();
            var errorsToConcat = Enumerable.Empty<IRError>();

            var rValueConcat = rListInitial.ConcatErrors(errorsToConcat);

            Assert.True(rValueConcat.Success);
            Assert.True(collection.SequenceEqual(rValueConcat.GetValue()));
        }
        
        /// <summary>
        /// Преобразование в значение
        /// </summary>
        [Fact]
        public void ToRValue_Ok()
        {
            var collection = GetRangeNumber();
            var rListInitial = collection.ToRList();

            var rValue = rListInitial.ToRValue();

            Assert.True(rValue.Success);
            Assert.True(rValue.GetValue().Equals(rListInitial.GetValue()));
        }

        /// <summary>
        /// Преобразование в значение
        /// </summary>
        [Fact]
        public void ToRValue_Error()
        {
            var initialError = CreateErrorTest();
            var rListInitial = initialError.ToRList<int>();

            var rValue = rListInitial.ToRValue();

            Assert.True(rValue.Failure);
            Assert.Single(rValue.GetErrors());
            Assert.True(initialError.Equals(rValue.GetErrors().Last()));
        }
    }
}