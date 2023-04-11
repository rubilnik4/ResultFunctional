using System;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.RExtension.GetValue()s;
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

            var result = number.ToRValueWhere(_ => true,
                                                   _ => CreateErrorTest());

            Assert.True(result.Success);
            Assert.Equal(number, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhere_BadError()
        {
            const int number = 1;
            var errorInitial = CreateErrorTest();
            var result = number.ToRValueWhere(_ => false,
                                                   _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public void ToResultValueWhereNull_Ok()
        {
            const string testString = "Test";
            var errorInitial = CreateErrorTest();
            var result = testString.ToRValueWhereNull(_ => true,
                                                           _ => errorInitial);

            Assert.True(result.Success);
            Assert.Equal(testString, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public void ToResultValueWhereNull_Bad()
        {
            string testString = String.Empty;
            var errorInitial = CreateErrorTest();
            var result = testString.ToRValueWhereNull(_ => false ,
                                                           _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public void ToResultValueWhereNull_Null()
        {
            string? testString = null;
            var errorInitial = CreateErrorTest();
            var result = testString.ToRValueWhereNull(_ => true,
                                                           _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullStruct_Ok()
        {
            int? testInt = 1;
            var errorInitial = CreateErrorTest();
            var result = testInt.ToRValueWhereNull(_ => true,
                                                        _ => errorInitial);

            Assert.True(result.Success);
            Assert.Equal(testInt, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullStruct_Bad()
        {
            int? testInt = 0;
            var errorInitial = CreateErrorTest();
            var result = testInt.ToRValueWhereNull(_ => false,
                                                        _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullStruct_Null()
        {
            int? testInt = null;
            var errorInitial = CreateErrorTest();
            var result = testInt.ToRValueWhereNull(_ => true,
                                                        _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullOkBad_Ok()
        {
            const string testString = "Test";

            var result = testString.ToRValueWhereNullOkBad(_ => true,
                                                            test => test,
                                                            _ => CreateErrorTest());

            Assert.True(result.Success);
            Assert.Equal(testString, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullOkBad_BadError()
        {
            string testString = String.Empty;
            var errorInitial = CreateErrorTest();

            var result = testString.ToRValueWhereNullOkBad(_ => false,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullOkBad_Null()
        {
            string? testString = null;
            var errorInitial = CreateErrorTest();

            var result = testString.ToRValueWhereNullOkBad(_ => true,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullOkBadStruct_Ok()
        {
            int? testInt = 1;

            var result = testInt.ToRValueWhereNullOkBad(_ => true,
                                                            test => test,
                                                            _ => CreateErrorTest());

            Assert.True(result.Success);
            Assert.Equal(testInt, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullOkBadStruct_BadError()
        {
            int? testInt = 0;
            var errorInitial = CreateErrorTest();

            var result = testInt.ToRValueWhereNullOkBad(_ => false,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public void ToResultValueWhereNullOkBadStruct_Null()
        {
            int? testInt = null;
            var errorInitial = CreateErrorTest();

            var result = testInt.ToRValueWhereNullOkBad(_ => true,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }
    }
}