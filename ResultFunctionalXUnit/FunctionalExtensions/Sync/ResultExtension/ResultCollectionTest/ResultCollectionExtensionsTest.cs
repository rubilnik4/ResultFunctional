using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultCollectionTest
{
    /// <summary>
    /// Методы расширения для результирующей коллекции со значением. Тесты
    /// </summary>
    public class ResultCollectionExtensionsTest
    {
        /// <summary>
        /// Проверить объект на нул. Без ошибок
        /// </summary>
        [Fact]
        public void ToResultCollectionNullCheck_Ok()
        {
            var collection = new List<string?> { "1", "2", "3" };

            var resultString = collection.ToResultCollectionNullCheck(CreateErrorTest());

            Assert.True(resultString.OkStatus);
            Assert.True(collection.SequenceEqual(resultString.Value));
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultCollectionNullCheck_ErrorCollectionNull()
        {
            List<string?>? collection = null;
            var initialError = CreateErrorTest();

            var resultString = collection.ToResultCollectionNullCheck(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError));
        }

        /// <summary>
        /// Проверить объект на нул. Ошибка нулевого значения
        /// </summary>
        [Fact]
        public void ToResultCollectionNullCheck_ErrorNull()
        {
            var collection = new List<string?> { "1", null, "3" };
            var initialError = CreateErrorTest();

            var resultString = collection.ToResultCollectionNullCheck(initialError);

            Assert.True(resultString.HasErrors);
            Assert.True(resultString.Errors.First().Equals(initialError));
        }
    }
}