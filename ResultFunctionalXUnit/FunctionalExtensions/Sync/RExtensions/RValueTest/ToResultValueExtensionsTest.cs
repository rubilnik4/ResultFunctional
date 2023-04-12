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
    public class ToResultValueExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public void ToResultCollection_OkStatus()
        {
            var collection = Enumerable.Range(0, 3).ToList();
            var resultNoError = collection.ToRValue();

            var resultCollection = resultNoError.ToRList();

            Assert.True(resultCollection.Success);
            Assert.True(collection.SequenceEqual(resultCollection.GetValue()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultValue_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = error.ToRValue<IEnumerable<int>>();

            var resultCollection = resultHasError.ToRList();

            Assert.True(resultCollection.Failure);
            Assert.Single(resultCollection.GetErrors());
            Assert.True(error.Equals(resultCollection.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public void ToResultCollection_Enumerable_OkStatus()
        {
            var collection = Enumerable.Range(0, 3).ToList();
            var resultValues = collection.Select(value => value.ToRValue());

            var resultCollection = resultValues.ToRList();

            Assert.True(resultCollection.Success);
            Assert.True(collection.SequenceEqual(resultCollection.GetValue()));
        }

        /// <summary>
        /// Вернуть результирующий ответ с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultCollection_Enumerable_HasErrors()
        {
            var error = CreateErrorTest();
            var collection = Enumerable.Range(0, 3).ToList().AsReadOnly();
            var resultValues = collection.Select(value => value.ToRValue()).
                                          Append(error.ToRValue<int>());

            var resultCollection = resultValues.ToRList();

            Assert.True(resultCollection.Failure);
            Assert.Single(resultCollection.GetErrors());
            Assert.True(error.Equals(resultCollection.GetErrors().Last()));
        }

        /// <summary>
        /// Проверить объект. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultValue_Ok()
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
        public void ToResultValueNullValueCheck_Ok()
        {
            const string initialString = "NotNull";

            var resultString = initialString.ToRValueNullValueCheck(CreateErrorTest());

            Assert.True(resultString.Success);
            Assert.Equal(initialString, resultString.GetValue());
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultValueNullValueCheck_ErrorNull()
        {
            const string? initialString = null;
            var initialError = CreateErrorTest();
            var resultString = initialString!.ToRValueNullValueCheck(initialError);

            Assert.True(resultString.Failure);
            Assert.True(resultString.GetErrors().First().Equals(initialError));
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultValueNullCheck_Ok()
        {
            const string initialString = "NotNull";

            var result = initialString.ToRValueNullCheck(CreateErrorTest());

            Assert.True(result.Success);
            Assert.Equal(initialString, result.GetValue());
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultValueNullCheck_ErrorNull()
        {
            const string? initialString = null;
            var initialError = CreateErrorTest();
            var result = initialString.ToRValueNullCheck(initialError);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(initialError));
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultValueNullCheckStruct_Ok()
        {
            int? initialInt = 1;

            var result = initialInt.ToRValueNullCheck(CreateErrorTest());

            Assert.True(result.Success);
            Assert.Equal(initialInt, result.GetValue());
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultValueNullCheckStruct_ErrorNull()
        {
            int? initialInt = null;
            var initialError = CreateErrorTest();
            var result = initialInt.ToRValueNullCheck(initialError);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(initialError));
        }


        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public void ToResultValueBind_OkStatus()    
        {
            var resultNoError = RUnitFactory.Some();
            const string value = "OkStatus";
            var resultValue = value.ToRValue();

            var resultValueAfter = resultNoError.ToRValueBind(resultValue);

            Assert.True(resultValueAfter.Success);
            Assert.Equal(value, resultValueAfter.GetValue());
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultValueBind_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = error.ToRUnit();
            const string value = "BadStatus";
            var resultValue = value.ToRValue();

            var resultValueAfter = resultHasError.ToRValueBind(resultValue);

            Assert.True(resultValueAfter.Failure);
            Assert.Single(resultValueAfter.GetErrors());
            Assert.True(error.Equals(resultValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением без ошибок
        /// </summary>      
        [Fact]
        public void ToResultValueFromResultError_OkStatus()
        {
            var resultNoError = RUnitFactory.Some();
            const string value = "OkStatus";

            var resultValue = resultNoError.ToRValue(value);

            Assert.True(resultValue.Success);
            Assert.Equal(value, resultValue.GetValue());
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultValueFromResultError_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = error.ToRUnit();
            const string value = "BadStatus";

            var resultValue = resultHasError.ToRValue(value);

            Assert.True(resultValue.Failure);
            Assert.Single(resultValue.GetErrors());
            Assert.True(error.Equals(resultValue.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой в результирующем ответе
        /// </summary>      
        [Fact]
        public void ToResultValueBind_HasErrorsBind()
        {
            var resultNoError = RUnitFactory.Some();
            var error = CreateErrorTest();
            var resultValue = error.ToRValue<string>();

            var resultValueAfter = resultNoError.ToRValueBind(resultValue);

            Assert.True(resultValueAfter.Failure);
            Assert.Single(resultValueAfter.GetErrors());
            Assert.True(error.Equals(resultValueAfter.GetErrors().Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultValueBind_HasErrorsBindInitial()
        {
            var error = CreateErrorTest();
            var resultHasError = error.ToRUnit();
            var errors = CreateErrorListTwoTest();
            var resultValue = errors.ToRValue<string>();

            var resultValueAfter = resultHasError.ToRValueBind(resultValue);

            Assert.True(resultValueAfter.Failure);
            Assert.Single(resultValueAfter.GetErrors());
            Assert.True(error.Equals(resultValueAfter.GetErrors().Last()));
        }
    }
}