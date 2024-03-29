﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Асинхронное преобразование внутреннего типа результирующего ответа со значением для функций высшего порядка для задачи-объекта. Тесты
    /// </summary>
    public class RValueCurryAwaitExtensionsTest
    {
        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_OkStatus_AddOkStatus()
        {
            int initialValue = Numbers.Number;

            var rValueFunc = RValueFactory.SomeTask(CurryFunctions.IntToStringAsync);
            var resultArgument = RValueFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal(initialValue.ToString(), await resultOut.GetValue());
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_OkStatus_AddBadStatus()
        {
            var rValueFunc = RValueFactory.SomeTask(CurryFunctions.IntToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = RValueFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_BadStatus_AddOkStatus()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.NoneTask<Func<int, Task<string>>>(errorFunc);
            var resultArgument = RValueFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для одного аргумента.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_BadStatus_AddBadStatus()
        {
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.NoneTask<Func<int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = RValueFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Equal(2, resultOut.GetErrors().Count);
            Assert.True(errorFunc.Equals(resultOut.GetErrors().First()));
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_OkStatus_AddOkStatus_TwoArguments()
        {
            int initialValue = Numbers.Number;
            var rValueFunc = RValueFactory.SomeTask(CurryFunctions.AggregateTwoToStringAsync);
            var resultArgument = RValueFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal((initialValue + initialValue).ToString(), await resultOut.GetValue().Invoke(initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_OkStatus_AddBadStatus_TwoArguments()
        {
            var rValueFunc = RValueFactory.SomeTask(CurryFunctions.AggregateTwoToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = RValueFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_BadStatus_AddOkStatus_TwoArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.NoneTask<Func<int, Task<string>>>(errorFunc);
            var resultArgument = RValueFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для двух аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_BadStatus_AddBadStatus_TwoArguments()
        {
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.NoneTask<Func<int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = RValueFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Equal(2, resultOut.GetErrors().Count);
            Assert.True(errorFunc.Equals(resultOut.GetErrors().First()));
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_OkStatus_AddOkStatus_ThreeArguments()
        {
            int initialValue = Numbers.Number;
            var rValueFunc = RValueFactory.SomeTask(CurryFunctions.AggregateThreeToStringAsync);
            var resultArgument = RValueFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal((initialValue * 3).ToString(), await resultOut.GetValue().Invoke(initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_OkStatus_AddBadStatus_ThreeArguments()
        {
            var rValueFunc = RValueFactory.SomeTask(CurryFunctions.AggregateThreeToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = RValueFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_BadStatus_AddOkStatus_ThreeArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.NoneTask<Func<int, Task<string>>>(errorFunc);
            var resultArgument = RValueFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для трех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_BadStatus_AddBadStatus_ThreeArguments()
        {
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.NoneTask<Func<int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = RValueFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Equal(2, resultOut.GetErrors().Count);
            Assert.True(errorFunc.Equals(resultOut.GetErrors().First()));
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_OkStatus_AddOkStatus_FourArguments()
        {
            int initialValue = Numbers.Number;
            var rValueFunc = RValueFactory.SomeTask(CurryFunctions.AggregateFourToStringAsync);
            var resultArgument = RValueFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal((initialValue * 4).ToString(), await resultOut.GetValue().Invoke(initialValue, initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_OkStatus_AddBadStatus_FourArguments()
        {
            var rValueFunc = RValueFactory.SomeTask(CurryFunctions.AggregateFourToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = RValueFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_BadStatus_AddOkStatus_FourArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.NoneTask<Func<int, Task<string>>>(errorFunc);
            var resultArgument = RValueFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для четырех аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_BadStatus_AddBadStatus_FourArguments()
        {
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.NoneTask<Func<int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = RValueFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Equal(2, resultOut.GetErrors().Count);
            Assert.True(errorFunc.Equals(resultOut.GetErrors().First()));
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ без ошибки. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_OkStatus_AddOkStatus_FiveArguments()
        {
            int initialValue = Numbers.Number;
            var rValueFunc = RValueFactory.SomeTask(CurryFunctions.AggregateFiveToStringAsync);
            var resultArgument = RValueFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Success);
            Assert.Equal((initialValue * 5).ToString(), await resultOut.GetValue().Invoke(initialValue, initialValue, initialValue, initialValue));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ без ошибки. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_OkStatus_AddBadStatus_FiveArguments()
        {
            var rValueFunc = RValueFactory.SomeTask(CurryFunctions.AggregateFiveToStringAsync);
            var errorArgument = CreateErrorTest();
            var resultArgument = RValueFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент без ошибки
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_BadStatus_AddOkStatus_FiveArguments()
        {
            int initialValue = Numbers.Number;
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.NoneTask<Func<int, Task<string>>>(errorFunc);
            var resultArgument = RValueFactory.SomeTask(initialValue);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Single(resultOut.GetErrors());
            Assert.True(errorFunc.Equals(resultOut.GetErrors().Last()));
        }

        /// <summary>
        /// Преобразование результирующего ответа с функцией высшего порядка для пяти аргументов.
        /// Ответ с ошибкой. Аргумент с ошибкой
        /// </summary>
        [Fact]
        public async Task RValueCurryOkBindAsync_BadStatus_AddBadStatus_FiveArguments()
        {
            var errorFunc = CreateErrorTest();
            var rValueFunc = RValueFactory.NoneTask<Func<int, Task<string>>>(errorFunc);
            var errorArgument = CreateErrorTest();
            var resultArgument = RValueFactory.NoneTask<int>(errorArgument);

            var resultOut = await rValueFunc.RValueCurryAwait(resultArgument);

            Assert.True(resultOut.Failure);
            Assert.Equal(2, resultOut.GetErrors().Count);
            Assert.True(errorFunc.Equals(resultOut.GetErrors().First()));
            Assert.True(errorArgument.Equals(resultOut.GetErrors().Last()));
        }
    }
}