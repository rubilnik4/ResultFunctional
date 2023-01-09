using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Преобразование внутреннего типа результирующего ответа со значением для функций высшего порядка для задачи-объекта. Тесты
    /// </summary>
    public class ResultValueCurryTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_OkStatus_AddOkStatus()
        {
            int initialValue = Numbers.Number;

            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.IntToStringAsync);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal(initialValue.ToString(), await resultOut.Value.Invoke());
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_OkStatus_AddBadStatus()
        {
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.IntToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_BadStatus_AddOkStatus()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<int, Task<string>>>(errorFunc);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_BadStatus_AddBadStatus()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

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
        public async Task ResultValueCurryOkTaskAsync_OkStatus_AddOkStatus_TwoArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateTwoToStringAsync);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal((initialValue + initialValue).ToString(), await resultOut.Value.Invoke(initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_OkStatus_AddBadStatus_TwoArguments()
        {
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateTwoToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_BadStatus_AddOkStatus_TwoArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<int, Task<string>>>(errorFunc);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_BadStatus_AddBadStatus_TwoArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

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
        public async Task ResultValueCurryOkTaskAsync_OkStatus_AddOkStatus_ThreeArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateThreeToStringAsync);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal((initialValue * 3).ToString(), await resultOut.Value.Invoke(initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_OkStatus_AddBadStatus_ThreeArguments()
        {
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateThreeToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_BadStatus_AddOkStatus_ThreeArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<int, Task<string>>>(errorFunc);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_BadStatus_AddBadStatus_ThreeArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

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
        public async Task ResultValueCurryOkTaskAsync_OkStatus_AddOkStatus_FourArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateFourToStringAsync);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal((initialValue * 4).ToString(), await resultOut.Value.Invoke(initialValue, initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_OkStatus_AddBadStatus_FourArguments()
        {
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateFourToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_BadStatus_AddOkStatus_FourArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<int, Task<string>>>(errorFunc);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_BadStatus_AddBadStatus_FourArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

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
        public async Task ResultValueCurryOkTaskAsync_OkStatus_AddOkStatus_FiveArguments()
        {
            int initialValue = Numbers.Number;
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateFiveToStringAsync);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal((initialValue * 5).ToString(), await resultOut.Value.Invoke(initialValue, initialValue, initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_OkStatus_AddBadStatus_FiveArguments()
        {
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateFiveToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_BadStatus_AddOkStatus_FiveArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<int, Task<string>>>(errorFunc);
            var resultArgument = new ResultValue<int>(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkTaskAsync_BadStatus_AddBadStatus_FiveArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultValue<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }
    }
}