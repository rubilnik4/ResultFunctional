using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Асинхронное преобразование внутреннего типа результирующего ответа со значением для функций высшего порядка. Тесты
    /// </summary>
    public class ResultValueCurryAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_OkStatus_AddOkStatus()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = new ResultValue<Func<int, Task<string>>>(CurryFunctions.IntToStringAsync);
            var resultArgument = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal(initialValue.ToString(), await resultOut.Value.Invoke());
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_OkStatus_AddBadStatus()
        {
            var resultValueFunc = new ResultValue<Func<int, Task<string>>>(CurryFunctions.IntToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultValueFactory.CreateTaskResultValueError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_BadStatus_AddOkStatus()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, Task<string>>>(errorFunc);
            var resultArgument = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_BadStatus_AddBadStatus()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultValueFactory.CreateTaskResultValueError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_OkStatus_AddOkStatus_TwoArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = new ResultValue<Func<int, int, Task<string>>>(CurryFunctions.AggregateTwoToStringAsync);
            var resultArgument = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal((initialValue + initialValue).ToString(), await resultOut.Value.Invoke(initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_OkStatus_AddBadStatus_TwoArguments()
        {
            var resultValueFunc = new ResultValue<Func<int, int, Task<string>>>(CurryFunctions.AggregateTwoToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultValueFactory.CreateTaskResultValueError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_BadStatus_AddOkStatus_TwoArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, Task<string>>>(errorFunc);
            var resultArgument = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_BadStatus_AddBadStatus_TwoArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultValueFactory.CreateTaskResultValueError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_OkStatus_AddOkStatus_ThreeArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = new ResultValue<Func<int, int, int, Task<string>>>(CurryFunctions.AggregateThreeToStringAsync);
            var resultArgument = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal((initialValue * 3).ToString(), await resultOut.Value.Invoke(initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_OkStatus_AddBadStatus_ThreeArguments()
        {
            var resultValueFunc = new ResultValue<Func<int, int, int, Task<string>>>(CurryFunctions.AggregateThreeToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultValueFactory.CreateTaskResultValueError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_BadStatus_AddOkStatus_ThreeArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, int, Task<string>>>(errorFunc);
            var resultArgument = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_BadStatus_AddBadStatus_ThreeArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultValueFactory.CreateTaskResultValueError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_OkStatus_AddOkStatus_FourArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = new ResultValue<Func<int, int, int, int, Task<string>>>(CurryFunctions.AggregateFourToStringAsync);
            var resultArgument = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal((initialValue * 4).ToString(), await resultOut.Value.Invoke(initialValue, initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_OkStatus_AddBadStatus_FourArguments()
        {
            var resultValueFunc = new ResultValue<Func<int, int, int, int, Task<string>>>(CurryFunctions.AggregateFourToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultValueFactory.CreateTaskResultValueError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_BadStatus_AddOkStatus_FourArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, int, int, Task<string>>>(errorFunc);
            var resultArgument = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_BadStatus_AddBadStatus_FourArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, int, int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultValueFactory.CreateTaskResultValueError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_OkStatus_AddOkStatus_FiveArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = new ResultValue<Func<int, int, int, int, int, Task<string>>>(CurryFunctions.AggregateFiveToStringAsync);
            var resultArgument = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal((initialValue * 5).ToString(), await resultOut.Value.Invoke(initialValue, initialValue, initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_OkStatus_AddBadStatus_FiveArguments()
        {
            var resultValueFunc = new ResultValue<Func<int, int, int, int, int, Task<string>>>(CurryFunctions.AggregateFiveToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultValueFactory.CreateTaskResultValueError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_BadStatus_AddOkStatus_FiveArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, int, int, int, Task<string>>>(errorFunc);
            var resultArgument = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkAsync_BadStatus_AddBadStatus_FiveArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<int, int, int, int, int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultValueFactory.CreateTaskResultValueError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }
    }
}