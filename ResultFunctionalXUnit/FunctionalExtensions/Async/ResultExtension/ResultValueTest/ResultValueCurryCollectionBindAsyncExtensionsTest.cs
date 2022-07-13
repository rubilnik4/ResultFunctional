using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Преобразование внутреннего типа результирующего ответа со значением для функций высшего порядка. Тесты
    /// </summary>
    public class ResultValueCurryCollectionTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_OkStatus_AddOkStatus()
        {
            var initialValue = Collections.GetRangeNumber();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.IntCollectionToString);
            var resultArgument = ResultCollectionFactory.CreateResultCollection(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal(CurryFunctions.IntCollectionToString(initialValue), resultOut.Value.Invoke());
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_OkStatus_AddBadStatus()
        {
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.IntCollectionToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultCollectionFactory.CreateResultCollectionError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_BadStatus_AddOkStatus()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<IEnumerable<int>, string>>(errorFunc);
            var resultArgument = ResultCollectionFactory.CreateResultCollection(Collections.GetRangeNumber());

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_BadStatus_AddBadStatus()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<IEnumerable<int>, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultCollectionFactory.CreateResultCollectionError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

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
        public async Task ResultValueCurryOkCollectionTaskAsync_OkStatus_AddOkStatus_TwoArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateCollectionTwoToString);
            var resultArgument = ResultCollectionFactory.CreateResultCollection(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal(CurryFunctions.AggregateCollectionTwoToString(initialValue, 2), resultOut.Value.Invoke(2));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_OkStatus_AddBadStatus_TwoArguments()
        {
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateCollectionTwoToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultCollectionFactory.CreateResultCollectionError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_BadStatus_AddOkStatus_TwoArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<IEnumerable<int>, int, string>>(errorFunc);
            var resultArgument = ResultCollectionFactory.CreateResultCollection(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_BadStatus_AddBadStatus_TwoArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<IEnumerable<int>, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultCollectionFactory.CreateResultCollectionError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

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
        public async Task ResultValueCurryOkCollectionTaskAsync_OkStatus_AddOkStatus_ThreeArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateCollectionThreeToString);
            var resultArgument = ResultCollectionFactory.CreateResultCollection(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal(CurryFunctions.AggregateCollectionThreeToString(initialValue, 3, 3), resultOut.Value.Invoke(3, 3));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_OkStatus_AddBadStatus_ThreeArguments()
        {
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateCollectionThreeToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultCollectionFactory.CreateResultCollectionError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_BadStatus_AddOkStatus_ThreeArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<IEnumerable<int>, int, int, string>>(errorFunc);
            var resultArgument = ResultCollectionFactory.CreateResultCollection(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_BadStatus_AddBadStatus_ThreeArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<IEnumerable<int>, int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultCollectionFactory.CreateResultCollectionError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

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
        public async Task ResultValueCurryOkCollectionTaskAsync_OkStatus_AddOkStatus_FourArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateCollectionFourToString);
            var resultArgument = ResultCollectionFactory.CreateResultCollection(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal(CurryFunctions.AggregateCollectionFourToString(initialValue, 4, 4, 4), resultOut.Value.Invoke(4, 4, 4));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_OkStatus_AddBadStatus_FourArguments()
        {
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateCollectionFourToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultCollectionFactory.CreateResultCollectionError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_BadStatus_AddOkStatus_FourArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<IEnumerable<int>, int, int, int, string>>(errorFunc);
            var resultArgument = ResultCollectionFactory.CreateResultCollection(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_BadStatus_AddBadStatus_FourArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<IEnumerable<int>, int, int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultCollectionFactory.CreateResultCollectionError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

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
        public async Task ResultValueCurryOkCollectionTaskAsync_OkStatus_AddOkStatus_FiveArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateCollectionFiveToString);
            var resultArgument = ResultCollectionFactory.CreateResultCollection(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal(CurryFunctions.AggregateCollectionFiveToString(initialValue, 5, 5, 5, 5), resultOut.Value.Invoke(5, 5, 5, 5));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_OkStatus_AddBadStatus_FiveArguments()
        {
            var resultValueFunc = ResultValueFactory.CreateTaskResultValue(CurryFunctions.AggregateCollectionFiveToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultCollectionFactory.CreateResultCollectionError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_BadStatus_AddOkStatus_FiveArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<IEnumerable<int>, int, int, int, int, string>>(errorFunc);
            var resultArgument = ResultCollectionFactory.CreateResultCollection(initialValue);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task ResultValueCurryOkCollectionTaskAsync_BadStatus_AddBadStatus_FiveArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = ResultValueFactory.CreateTaskResultValueError<Func<IEnumerable<int>, int, int, int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = ResultCollectionFactory.CreateResultCollectionError<int>(errorArgument);

            var resultOut = await resultValueFunc.ResultValueCurryCollectionOkTaskAsync(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }
    }
}