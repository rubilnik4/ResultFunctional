using System;
using System.Collections.Generic;
using System.Linq;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Преобразование внутреннего типа результирующего ответа со значением для функций высшего порядка. Тесты
    /// </summary>
    public class ResultValueCurryCollectionExtensionsTest
    {
        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_OkStatus_AddOkStatus()
        {
            var initialValue = Collections.GetRangeNumber();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, string>>(CurryFunctions.IntCollectionToString);
            var resultArgument = new ResultCollection<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal(CurryFunctions.IntCollectionToString(initialValue), resultOut.Value.Invoke());
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_OkStatus_AddBadStatus()
        {
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, string>>(CurryFunctions.IntCollectionToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultCollection<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_BadStatus_AddOkStatus()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, string>>(errorFunc);
            var resultArgument = new ResultCollection<int>(Collections.GetRangeNumber());

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_BadStatus_AddBadStatus()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultCollection<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

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
        public void ResultValueCurryOkCollection_OkStatus_AddOkStatus_TwoArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, string>>(CurryFunctions.AggregateCollectionTwoToString);
            var resultArgument = new ResultCollection<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal(CurryFunctions.AggregateCollectionTwoToString(initialValue, 2), resultOut.Value.Invoke(2));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_OkStatus_AddBadStatus_TwoArguments()
        {
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, string>>(CurryFunctions.AggregateCollectionTwoToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultCollection<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_BadStatus_AddOkStatus_TwoArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, string>>(errorFunc);
            var resultArgument = new ResultCollection<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_BadStatus_AddBadStatus_TwoArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultCollection<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

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
        public void ResultValueCurryOkCollection_OkStatus_AddOkStatus_ThreeArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, int, string>>(CurryFunctions.AggregateCollectionThreeToString);
            var resultArgument = new ResultCollection<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal(CurryFunctions.AggregateCollectionThreeToString(initialValue, 3, 3), resultOut.Value.Invoke(3, 3));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_OkStatus_AddBadStatus_ThreeArguments()
        {
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, int, string>>(CurryFunctions.AggregateCollectionThreeToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultCollection<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_BadStatus_AddOkStatus_ThreeArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, int, string>>(errorFunc);
            var resultArgument = new ResultCollection<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_BadStatus_AddBadStatus_ThreeArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultCollection<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

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
        public void ResultValueCurryOkCollection_OkStatus_AddOkStatus_FourArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, int, int, string>>(CurryFunctions.AggregateCollectionFourToString);
            var resultArgument = new ResultCollection<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal(CurryFunctions.AggregateCollectionFourToString(initialValue, 4, 4, 4), resultOut.Value.Invoke(4, 4, 4));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_OkStatus_AddBadStatus_FourArguments()
        {
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, int, int, string>>(CurryFunctions.AggregateCollectionFourToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultCollection<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_BadStatus_AddOkStatus_FourArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, int, int, string>>(errorFunc);
            var resultArgument = new ResultCollection<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_BadStatus_AddBadStatus_FourArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultCollection<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

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
        public void ResultValueCurryOkCollection_OkStatus_AddOkStatus_FiveArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, int, int, int, string>>(CurryFunctions.AggregateCollectionFiveToString);
            var resultArgument = new ResultCollection<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.OkStatus);
            Assert.Equal(CurryFunctions.AggregateCollectionFiveToString(initialValue, 5, 5, 5, 5), resultOut.Value.Invoke(5, 5, 5, 5));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_OkStatus_AddBadStatus_FiveArguments()
        {
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, int, int, int, string>>(CurryFunctions.AggregateCollectionFiveToString);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultCollection<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_BadStatus_AddOkStatus_FiveArguments()
        {
            var initialValue = Collections.GetRangeNumber();
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, int, int, int, string>>(errorFunc);
            var resultArgument = new ResultCollection<int>(initialValue);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Single(resultOut.Errors);
            Assert.True(errorFunc.Equals(resultOut.Errors.Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public void ResultValueCurryOkCollection_BadStatus_AddBadStatus_FiveArguments()
        {
            var errorFunc = CreateErrorTest();
            var resultValueFunc = new ResultValue<Func<IEnumerable<int>, int, int, int, int, string>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = new ResultCollection<int>(errorArgument);

            var resultOut = resultValueFunc.ResultValueCurryCollectionOk(resultArgument);

            Assert.True(resultOut.HasErrors);
            Assert.Equal(2, resultOut.Errors.Count);
            Assert.True(errorFunc.Equals(resultOut.Errors.First()));
            Assert.True(errorArgument.Equals(resultOut.Errors.Last()));
        }
    }
}