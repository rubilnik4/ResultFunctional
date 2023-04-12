using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Преобразование значения в результирующий ответ с условием для задачи-объекта асинхронно. Тесты
    /// </summary>
    public class ToResultValueWhereBindAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereBindAsync_Ok()
        {
            const int number = 1;

            var result = await Task.FromResult(number).ToResultValueWhereBindAsync(_ => true,
                                                                              _ => CreateErrorTestTask());

            Assert.True(result.Success);
            Assert.Equal(number, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereBindAsync_BadError()
        {
            const int number = 1;
            var errorInitial = CreateErrorTest();

            var result = await Task.FromResult(number).ToResultValueWhereBindAsync(_ => false,
                                                                              _ => Task.FromResult(errorInitial));

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullBindAsync_Ok()
        {
            var testString = StringTest.TestStringTask;
            var errorInitial = CreateErrorTestTask();
            var result = await testString.ToResultValueWhereNullBindAsync(_ => true,
                                                                          _ => errorInitial);

            Assert.True(result.Success);
            Assert.Equal(testString.Result, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullBindAsync_Bad()
        {
            var testString = StringTest.TestStringTask;
            var errorInitial = CreateErrorTestTask();
            var result = await testString.ToResultValueWhereNullBindAsync(_ => false,
                                                           _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullBindAsync_Null()
        {
            var testString = StringTest.TestStringTaskNull;
            var errorInitial = CreateErrorTestTask();
            var result = await testString.ToResultValueWhereNullBindAsync(_ => true,
                                                           _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullStructBindAsync_Ok()
        {
            var testInt = Numbers.NumberTask;
            var errorInitial = CreateErrorTestTask();
            var result = await testInt.ToResultValueWhereNullBindAsync(_ => true,
                                                        _ => errorInitial);

            Assert.True(result.Success);
            Assert.Equal(testInt.Result, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullStructBindAsync_Bad()
        {
            var testInt = Numbers.NumberTask;
            var errorInitial = CreateErrorTestTask();
            var result = await testInt.ToResultValueWhereNullBindAsync(_ => false,
                                                        _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullStructBindAsync_Null()
        {
            var testInt = Numbers.NumberTaskNull;
            var errorInitial = CreateErrorTestTask();
            var result = await testInt.ToResultValueWhereNullBindAsync(_ => true,
                                                        _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadBindAsync_Ok()
        {
            var testString = StringTest.TestStringTask;

            var result = await testString.ToResultValueWhereNullOkBadBindAsync(_ => true,
                                                                           Task.FromResult,
                                                                           _ => CreateErrorTestTask());

            Assert.True(result.Success);
            Assert.Equal(testString.Result, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadBindAsync_BadError()
        {
            var testString = StringTest.TestStringTask;
            var errorInitial = CreateErrorTestTask();

            var result = await testString.ToResultValueWhereNullOkBadBindAsync(_ => false,
                                                            Task.FromResult,
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadBindAsync_Null()
        {
            var testString = StringTest.TestStringTaskNull;
            var errorInitial = CreateErrorTestTask();

            var result = await testString.ToResultValueWhereNullOkBadBindAsync(_ => true,
                                                            Task.FromResult,
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadStructBindAsync_Ok()
        {
            var testInt = Numbers.NumberTask;

            var result = await testInt.ToResultValueWhereNullOkBadBindAsync(_ => true,
                                                            CurryFunctions.IntToStringAsync,
                                                            _ => CreateErrorTestTask());

            Assert.True(result.Success);
            Assert.Equal(testInt.Result.ToString(), result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadStructBindAsync_BadError()
        {
            var testInt = Numbers.NumberTask;
            var errorInitial = CreateErrorTestTask();

            var result = await testInt.ToResultValueWhereNullOkBadBindAsync(_ => false,
                                                            CurryFunctions.IntToStringAsync,
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadStructBindAsync_Null()
        {
            var testInt = Numbers.NumberTaskNull;
            var errorInitial = CreateErrorTestTask();

            var result = await testInt.ToResultValueWhereNullOkBadBindAsync(_ => true,
                                                            CurryFunctions.IntToStringAsync,
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial.Result));
        }
    }
}