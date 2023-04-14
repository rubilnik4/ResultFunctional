using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
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

            var result = await Task.FromResult(number).ToRValueOptionTask(_ => true,
                                                                              _ => CreateErrorTest());

            Assert.True(result.Success);
            Assert.Equal(number, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereTaskAsync_BadError()
        {
            const int number = 1;
            var errorInitial = CreateErrorTest();

            var result = await Task.FromResult(number).ToRValueOptionTask(_ => false,
                                                                              _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }


        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullTaskAsync_Ok()
        {
            var testString = StringTest.TestStringTask;
            var errorInitial = CreateErrorTest();
            var result = await testString.ToRValueEnsureOptionTask(_ => true,
                                                           _ => errorInitial);

            Assert.True(result.Success);
            Assert.Equal(testString.Result, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullTaskAsync_Bad()
        {
            var testString = StringTest.TestStringTask;
            var errorInitial = CreateErrorTest();
            var result = await testString.ToRValueEnsureOptionTask(_ => false,
                                                           _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullTaskAsync_Null()
        {
            var testString = StringTest.TestStringTaskNull;
            var errorInitial = CreateErrorTest();
            var result = await testString.ToRValueEnsureOptionTask(_ => true,
                                                           _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullStructTaskAsync_Ok()
        {
            var testInt = Numbers.NumberTask;
            var errorInitial = CreateErrorTest();
            var result = await testInt.ToRValueEnsureOptionTask(_ => true,
                                                        _ => errorInitial);

            Assert.True(result.Success);
            Assert.Equal(testInt.Result, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullStructTaskAsync_Bad()
        {
            var testInt = Numbers.NumberTask;
            var errorInitial = CreateErrorTest();
            var result = await testInt.ToRValueEnsureOptionTask(_ => false,
                                                        _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием и проверкой на нуль
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullStructTaskAsync_Null()
        {
            var testInt = Numbers.NumberTaskNull;
            var errorInitial = CreateErrorTest();
            var result = await testInt.ToRValueEnsureOptionTask(_ => true,
                                                        _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadTaskAsync_Ok()
        {
            var testString = StringTest.TestStringTask;

            var result = await testString.ToRValueEnsureWhereTask(_ => true,
                                                            test => test,
                                                            _ => CreateErrorTest());

            Assert.True(result.Success);
            Assert.Equal(testString.Result, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadTaskAsync_BadError()
        {
            var testString = StringTest.TestStringTask;
            var errorInitial = CreateErrorTest();

            var result = await testString.ToRValueEnsureWhereTask(_ => false,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadTaskAsync_Null()
        {
            var testString = StringTest.TestStringTaskNull;
            var errorInitial = CreateErrorTest();

            var result = await testString.ToRValueEnsureWhereTask(_ => true,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Положительное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadStructTaskAsync_Ok()
        {
            var testInt = Numbers.NumberTask;

            var result = await testInt.ToRValueEnsureWhereTask(_ => true,
                                                            test => test,
                                                            _ => CreateErrorTest());

            Assert.True(result.Success);
            Assert.Equal(testInt.Result, result.GetValue());
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadStructTaskAsync_BadError()
        {
            var testInt = Numbers.NumberTask;
            var errorInitial = CreateErrorTest();

            var result = await testInt.ToRValueEnsureWhereTask(_ => false,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }

        /// <summary>
        /// Преобразовать значения в результирующий ответ с условием. Негативное условие
        /// </summary>
        [Fact]
        public async Task ToResultValueWhereNullOkBadStructTaskAsync_Null()
        {
            var testInt = Numbers.NumberTaskNull;
            var errorInitial = CreateErrorTest();

            var result = await testInt.ToRValueEnsureWhereTask(_ => true,
                                                            test => test.ToString(),
                                                            _ => errorInitial);

            Assert.True(result.Failure);
            Assert.True(result.GetErrors().First().Equals(errorInitial));
        }
    }
}