using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Методы расширения для результирующего ответа со значением. Тесты
    /// </summary>
    public class ResultValueExtensionsTest
    {
        /// <summary>
        /// Вернуть результирующий ответ с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public void ToResultCollection_OkStatus()
        {
            var collection = Enumerable.Range(0, 3).ToList().AsReadOnly();
            var resultNoError = new ResultValue<IEnumerable<int>>(collection);

            var resultCollection = resultNoError.ToResultCollection();

            Assert.True(resultCollection.OkStatus);
            Assert.True(collection.SequenceEqual(resultCollection.Value));
        }

        /// <summary>
        /// Вернуть результирующий ответ со значением с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultValue_HasErrors()
        {
            var error = CreateErrorTest();
            var resultHasError = new ResultValue<IEnumerable<int>>(error);

            var resultCollection = resultHasError.ToResultCollection();

            Assert.True(resultCollection.HasErrors);
            Assert.Single(resultCollection.Errors);
            Assert.True(error.Equals(resultCollection.Errors.Last()));
        }

        /// <summary>
        /// Вернуть результирующий ответ с коллекцией без ошибок
        /// </summary>      
        [Fact]
        public void ToResultCollection_Enumerable_OkStatus()
        {
            var collection = Enumerable.Range(0, 3).ToList().AsReadOnly();
            var resultValues = collection.Select(value => new ResultValue<int>(value));

            var resultCollection = resultValues.ToResultCollection();

            Assert.True(resultCollection.OkStatus);
            Assert.True(collection.SequenceEqual(resultCollection.Value));
        }

        /// <summary>
        /// Вернуть результирующий ответ с коллекцией с ошибкой
        /// </summary>      
        [Fact]
        public void ToResultCollection_Enumerable_HasErrors()
        {
            var error = CreateErrorTest();
            var collection = Enumerable.Range(0, 3).ToList().AsReadOnly();
            var resultValues = collection.Select(value => new ResultValue<int>(value)).
                                          Append(new ResultValue<int>(error));

            var resultCollection = resultValues.ToResultCollection();

            Assert.True(resultCollection.HasErrors);
            Assert.Single(resultCollection.Errors);
            Assert.True(error.Equals(resultCollection.Errors.Last()));
        }

        /// <summary>
        /// Проверить объект. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultValue_Ok()
        {
            const string initialString = "NotNull";

            var resultString = initialString.ToResultValue();

            Assert.True(resultString.OkStatus);
            Assert.Equal(initialString, resultString.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultValueNullValueCheck_Ok()
        {
            const string initialString = "NotNull";

            var resultString = initialString.ToResultValueNullValueCheck(CreateErrorTest());

            Assert.True(resultString.OkStatus);
            Assert.Equal(initialString, resultString.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultValueNullValueCheck_ErrorNull()
        {
            const string? initialString = null;
            var initialError = CreateErrorTest();
            var resultString = initialString!.ToResultValueNullValueCheck(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultValueNullCheck_Ok()
        {
            const string initialString = "NotNull";

            var result = initialString.ToResultValueNullCheck(CreateErrorTest());

            Assert.True(result.OkStatus);
            Assert.Equal(initialString, result.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultValueNullCheck_ErrorNull()
        {
            const string? initialString = null;
            var initialError = CreateErrorTest();
            var result = initialString.ToResultValueNullCheck(initialError);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultValueNullCheckStruct_Ok()
        {
            int? initialInt = 1;

            var result = initialInt.ToResultValueNullCheck(CreateErrorTest());

            Assert.True(result.OkStatus);
            Assert.Equal(initialInt, result.Value);
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultValueNullCheckStruct_ErrorNull()
        {
            int? initialInt = null;
            var initialError = CreateErrorTest();
            var result = initialInt.ToResultValueNullCheck(initialError);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(initialError));
        }
    }
}