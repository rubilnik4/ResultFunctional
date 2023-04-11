using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.ResultValues;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием асинхронно. Тесты
    /// </summary>
    public class ToResultValueWhereAsyncAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereAsync_Ok()
        {
            const int number = 1;

            var result = await number.ToResultValueWhereAsync(_ => true,
                                                              _ => CreateErrorTestTask());

            Assert.True(result.Success);
            Assert.Equal(number, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereAsync_BadError()
        {
            const int number = 1;
            var errorInitial = CreateErrorTest();
            var result = await number.ToResultValueWhereAsync(_ => false,
                                                              _ => Task.FromResult(errorInitial));

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullAsync_Ok()
        {
            const string testString = "Test";
            var errorInitial = CreateErrorTestTask();
            var result = await testString.ToResultValueWhereNullAsync(_ => true,
                                                                _ => errorInitial);

            Assert.True(result.Success);
            Assert.Equal(testString, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullAsync_Bad()
        {
            string testString = String.Empty;
            var errorInitial = CreateErrorTestTask();
            var result = await testString.ToResultValueWhereNullAsync(_ => false,
                                                           _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullAsync_Null()
        {
            string? testString = null;
            var errorInitial = CreateErrorTestTask();
            var result = await testString.ToResultValueWhereNullAsync(_ => true,
                                                           _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullStructAsync_Ok()
        {
            int? testInt = 1;
            var errorInitial = CreateErrorTestTask();
            var result = await testInt.ToResultValueWhereNullAsync(_ => true,
                                                        _ => errorInitial);

            Assert.True(result.Success);
            Assert.Equal(testInt, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullStructAsync_Bad()
        {
            int? testInt = 0;
            var errorInitial = CreateErrorTestTask();
            var result = await testInt.ToResultValueWhereNullAsync(_ => false,
                                                        _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullStructAsync_Null()
        {
            int? testInt = null;
            var errorInitial = CreateErrorTestTask();
            var result = await testInt.ToResultValueWhereNullAsync(_ => true,
                                                        _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadAsync_Ok()
        {
            const string testString = "Test";

            var result = await testString.ToResultValueWhereNullOkBadAsync(_ => true,
                                                                           Task.FromResult,
                                                                           _ => CreateErrorTestTask());

            Assert.True(result.Success);
            Assert.Equal(testString, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadAsync_BadError()
        {
            string testString = String.Empty;
            var errorInitial = CreateErrorTestTask();

            var result = await testString.ToResultValueWhereNullOkBadAsync(_ => false,
                                                            Task.FromResult,
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadAsync_Null()
        {
            string? testString = null;
            var errorInitial = CreateErrorTestTask();

            var result = await testString.ToResultValueWhereNullOkBadAsync(_ => true,
                                                            Task.FromResult,
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadStructAsync_Ok()
        {
            int? testInt = 1;

            var result = await testInt.ToResultValueWhereNullOkBadAsync(_ => true,
                                                            CurryFunctions.IntToStringAsync,
                                                            _ => CreateErrorTestTask());

            Assert.True(result.Success);
            Assert.Equal(testInt.ToString(), result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadStructAsync_BadError()
        {
            int? testInt = 0;
            var errorInitial = CreateErrorTestTask();

            var result = await testInt.ToResultValueWhereNullOkBadAsync(_ => false,
                                                            CurryFunctions.IntToStringAsync,
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadStructAsync_Null()
        {
            int? testInt = null;
            var errorInitial = CreateErrorTestTask();

            var result = await testInt.ToResultValueWhereNullOkBadAsync(_ => true,
                                                            CurryFunctions.IntToStringAsync,
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }
    }
}