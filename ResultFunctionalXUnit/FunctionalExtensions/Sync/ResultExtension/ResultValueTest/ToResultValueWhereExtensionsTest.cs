using System;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием. Тесты
    /// </summary>
    public class ToResultValueWhereAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhere_Ok()
        {
            const int number = 1;

            var result = number.ToResultValueWhere(_ => true,
                                                   _ => CreateErrorTest());

            Assert.True(result.OkStatus);
            Assert.Equal(number, result.Value);
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhere_BadError()
        {
            const int number = 1;
            var errorInitial = CreateErrorTest();
            var result = number.ToResultValueWhere(_ => false,
                                                   _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public void ToResultValueWhereNull_Ok()
        {
            const string testString = "Test";
            var errorInitial = CreateErrorTest();
            var result = testString.ToResultValueWhereNull(_ => true,
                                                           _ => errorInitial);

            Assert.True(result.OkStatus);
            Assert.Equal(testString, result.Value);
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public void ToResultValueWhereNull_Bad()
        {
            string testString = String.Empty;
            var errorInitial = CreateErrorTest();
            var result = testString.ToResultValueWhereNull(_ => false ,
                                                           _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public void ToResultValueWhereNull_Null()
        {
            string? testString = null;
            var errorInitial = CreateErrorTest();
            var result = testString.ToResultValueWhereNull(_ => true,
                                                           _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullStruct_Ok()
        {
            int? testInt = 1;
            var errorInitial = CreateErrorTest();
            var result = testInt.ToResultValueWhereNull(_ => true,
                                                        _ => errorInitial);

            Assert.True(result.OkStatus);
            Assert.Equal(testInt, result.Value);
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullStruct_Bad()
        {
            int? testInt = 0;
            var errorInitial = CreateErrorTest();
            var result = testInt.ToResultValueWhereNull(_ => false,
                                                        _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullStruct_Null()
        {
            int? testInt = null;
            var errorInitial = CreateErrorTest();
            var result = testInt.ToResultValueWhereNull(_ => true,
                                                        _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullOkBad_Ok()
        {
            const string testString = "Test";

            var result = testString.ToResultValueWhereNullOkBad(_ => true,
                                                            test => test,
                                                            _ => CreateErrorTest());

            Assert.True(result.OkStatus);
            Assert.Equal(testString, result.Value);
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullOkBad_BadError()
        {
            string testString = String.Empty;
            var errorInitial = CreateErrorTest();

            var result = testString.ToResultValueWhereNullOkBad(_ => false,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullOkBad_Null()
        {
            string? testString = null;
            var errorInitial = CreateErrorTest();

            var result = testString.ToResultValueWhereNullOkBad(_ => true,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullOkBadStruct_Ok()
        {
            int? testInt = 1;

            var result = testInt.ToResultValueWhereNullOkBad(_ => true,
                                                            test => test,
                                                            _ => CreateErrorTest());

            Assert.True(result.OkStatus);
            Assert.Equal(testInt, result.Value);
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullOkBadStruct_BadError()
        {
            int? testInt = 0;
            var errorInitial = CreateErrorTest();

            var result = testInt.ToResultValueWhereNullOkBad(_ => false,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullOkBadStruct_Null()
        {
            int? testInt = null;
            var errorInitial = CreateErrorTest();

            var result = testInt.ToResultValueWhereNullOkBad(_ => true,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }
    }
}