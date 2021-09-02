using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием для задачи-объекта. Тесты
    /// </summary>
    public class ToResultValueWhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereTaskAsync_Ok()
        {
            const int number = 1;

            var result = await Task.FromResult(number).ToResultValueWhereTaskAsync(_ => true,
                                                                              _ => CreateErrorTest());

            Assert.True(result.OkStatus);
            Assert.Equal(number, result.Value);
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereTaskAsync_BadError()
        {
            const int number = 1;
            var errorInitial = CreateErrorTest();

            var result = await Task.FromResult(number).ToResultValueWhereTaskAsync(_ => false,
                                                                              _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }


        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullTaskAsync_Ok()
        {
            var testString = StringTest.TestStringTask;
            var errorInitial = CreateErrorTest();
            var result = await testString.ToResultValueWhereNullTaskAsync(_ => true,
                                                           _ => errorInitial);

            Assert.True(result.OkStatus);
            Assert.Equal(testString.Result, result.Value);
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullTaskAsync_Bad()
        {
            var testString = StringTest.TestStringTask;
            var errorInitial = CreateErrorTest();
            var result = await testString.ToResultValueWhereNullTaskAsync(_ => false,
                                                           _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullTaskAsync_Null()
        {
            var testString = StringTest.TestStringTaskNull;
            var errorInitial = CreateErrorTest();
            var result = await testString.ToResultValueWhereNullTaskAsync(_ => true,
                                                           _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullStructTaskAsync_Ok()
        {
            var testInt = Numbers.NumberTask;
            var errorInitial = CreateErrorTest();
            var result = await testInt.ToResultValueWhereNullTaskAsync(_ => true,
                                                        _ => errorInitial);

            Assert.True(result.OkStatus);
            Assert.Equal(testInt.Result, result.Value);
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullStructTaskAsync_Bad()
        {
            var testInt = Numbers.NumberTask;
            var errorInitial = CreateErrorTest();
            var result = await testInt.ToResultValueWhereNullTaskAsync(_ => false,
                                                        _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullStructTaskAsync_Null()
        {
            var testInt = Numbers.NumberTaskNull;
            var errorInitial = CreateErrorTest();
            var result = await testInt.ToResultValueWhereNullTaskAsync(_ => true,
                                                        _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadTaskAsync_Ok()
        {
            var testString = StringTest.TestStringTask;

            var result = await testString.ToResultValueWhereNullOkBadTaskAsync(_ => true,
                                                            test => test,
                                                            _ => CreateErrorTest());

            Assert.True(result.OkStatus);
            Assert.Equal(testString.Result, result.Value);
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadTaskAsync_BadError()
        {
            var testString = StringTest.TestStringTask;
            var errorInitial = CreateErrorTest();

            var result = await testString.ToResultValueWhereNullOkBadTaskAsync(_ => false,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadTaskAsync_Null()
        {
            var testString = StringTest.TestStringTaskNull;
            var errorInitial = CreateErrorTest();

            var result = await testString.ToResultValueWhereNullOkBadTaskAsync(_ => true,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadStructTaskAsync_Ok()
        {
            var testInt = Numbers.NumberTask;

            var result = await testInt.ToResultValueWhereNullOkBadTaskAsync(_ => true,
                                                            test => test,
                                                            _ => CreateErrorTest());

            Assert.True(result.OkStatus);
            Assert.Equal(testInt.Result, result.Value);
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadStructTaskAsync_BadError()
        {
            var testInt = Numbers.NumberTask;
            var errorInitial = CreateErrorTest();

            var result = await testInt.ToResultValueWhereNullOkBadTaskAsync(_ => false,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadStructTaskAsync_Null()
        {
            var testInt = Numbers.NumberTaskNull;
            var errorInitial = CreateErrorTest();

            var result = await testInt.ToResultValueWhereNullOkBadTaskAsync(_ => true,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.HasErrors);
            Assert.True(result.Errors.First().Equals(errorInitial));
        }
    }
}