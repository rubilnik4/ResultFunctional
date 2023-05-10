﻿using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RMaybeTest;

public class RMaybeOptionAsyncExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkAsync_Ok_CheckNoError()
    {
        var rMaybe = RUnitFactory.Some();

        var resultAfterWhere = await rMaybe.RUnitEnsureAsync(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Success);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkAsync_Ok_CheckHasError()
    {
        var rMaybe = RUnitFactory.Some();

        var errorBad = CreateErrorListTwoTestTask();
        var resultAfterWhere = await rMaybe.RUnitEnsureAsync(() => false,
                                                                               () => errorBad);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errorBad.Result.Count, resultAfterWhere.GetErrors().Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkAsync_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var rMaybe = RUnitFactory.None(errorInitial);

        var resultAfterWhere = await rMaybe.RUnitEnsureAsync(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkAsync_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var rMaybe = RUnitFactory.None(errorsInitial);

        var resultAfterWhere = await rMaybe.RUnitEnsureAsync(() => false,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }
}