using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Lists;
using ResultFunctional.FunctionalExtensions.Sync.RExtensions.Values;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.RExtensions.RValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением. Тесты
    /// </summary>
    public class ToRValueExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public void ToRList_OkStatus()
        {
            var collection = Enumerable.Range(0, 3).ToList();
            var resultNoError = collection.ToRValue();

            var rList = resultNoError.ToRList();

            Assert.True(rList.Success);
            Assert.True(collection.SequenceEqual(rList.GetValue()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToRValue_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = error.ToRValue<IEnumerable<int>>();

            var rList = resultHasError.ToRList();

            Assert.True(rList.Failure);
            Assert.Single(rList.GetErrors());
            Assert.True(error.Equals(rList.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public void ToRList_Enumerable_OkStatus()
        {
            var collection = Enumerable.Range(0, 3).ToList();
            var rValues = collection.Select(value => value.ToRValue());

            var rList = rValues.ToRList();

            Assert.True(rList.Success);
            Assert.True(collection.SequenceEqual(rList.GetValue()));
        }

        /// <summary>
        /// Вернуть результирующий ответ с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public void ToRList_Enumerable_HasErrors()
        {
            var error = CreateErrorTest();
            var collection = Enumerable.Range(0, 3).ToList().AsReadOnly();
            var rValues = collection.Select(value => value.ToRValue()).
                                          Append(error.ToRValue<int>());

            var rList = rValues.ToRList();

            Assert.True(rList.Failure);
            Assert.Single(rList.GetErrors());
            Assert.True(error.Equals(rList.GetErrors().Last()));
        }

        /// <summary>
        /// Проверить объект. Без ошибок
        /// </summary>
        [Fact]
        public void ToRValue_Ok()
        {
            const string initialString = "NotNull";

            var resultString = initialString.ToRValue();

            Assert.True(resultString.Success);
            Assert.Equal(initialString, resultString.GetValue());
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public void ToRValueNullValueCheck_Ok()
        {
            const string initialString = "NotNull";

            var resultString = initialString.ToRValueEnsure(CreateErrorTest());

            Assert.True(resultString.Success);
            Assert.Equal(initialString, resultString.GetValue());
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToRValueNullValueCheck_ErrorNull()
        {
            const string? initialString = null;
            var initialError = CreateErrorTest();
            var resultString = initialString!.ToRValueEnsure(initialError);

            Assert.True(resultString.Failure);
            Assert.True(resultString.GetErrors().First().Equals(initialError));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public void ToRValueBind_OkStatus()    
        {
            var resultNoError = RUnitFactory.Some();
            const string value = "OkStatus";
            var rValue = value.ToRValue();

            var rValueAfter = resultNoError.ToRValueBind(rValue);

            Assert.True(rValueAfter.Success);
            Assert.Equal(value, rValueAfter.GetValue());
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToRValueBind_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = error.ToRUnit();
            const string value = "BadStatus";
            var rValue = value.ToRValue();

            var rValueAfter = resultHasError.ToRValueBind(rValue);

            Assert.True(rValueAfter.Failure);
            Assert.Single(rValueAfter.GetErrors());
            Assert.True(error.Equals(rValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public void ToRValueFromRMaybe_OkStatus()
        {
            var resultNoError = RUnitFactory.Some();
            const string value = "OkStatus";

            var rValue = resultNoError.MaybeRValue(value);

            Assert.True(rValue.Success);
            Assert.Equal(value, rValue.GetValue());
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToRValueFromRMaybe_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = error.ToRUnit();
            const string value = "BadStatus";

            var rValue = resultHasError.MaybeRValue(value);

            Assert.True(rValue.Failure);
            Assert.Single(rValue.GetErrors());
            Assert.True(error.Equals(rValue.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой в результирующем ответе
        /// </summary>      
        [Fact]
        public void ToRValueBind_HasErrorsBind()
        {
            var resultNoError = RUnitFactory.Some();
            var error = CreateErrorTest();
            var rValue = error.ToRValue<string>();

            var rValueAfter = resultNoError.ToRValueBind(rValue);

            Assert.True(rValueAfter.Failure);
            Assert.Single(rValueAfter.GetErrors());
            Assert.True(error.Equals(rValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToRValueBind_HasErrorsBindInitial()
        {
            var error = CreateErrorTest();
            var resultHasError = error.ToRUnit();
            var errors = CreateErrorListTwoTest();
            var rValue = errors.ToRValue<string>();

            var rValueAfter = resultHasError.ToRValueBind(rValue);

            Assert.True(rValueAfter.Failure);
            Assert.Single(rValueAfter.GetErrors());
            Assert.True(error.Equals(rValueAfter.GetErrors().Last()));
        }
    }
}