﻿using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RMaybeTest;

public class RMaybeOptionAwaitExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkBindAsync_Ok_CheckNoError()
    {
        var rMaybe = RUnitFactory.SomeTask();

        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeEnsureAwait(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Success);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkBindAsync_Ok_CheckHasError()
    {
        var rMaybe = RUnitFactory.SomeTask();

        var errorBad = CreateErrorListTwoTestTask();
        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeEnsureAwait(() => false,
                                                                               () => errorBad);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errorBad.Result.Count, resultAfterWhere.GetErrors().Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkBindAsync_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var rMaybe = RUnitFactory.NoneTask(errorInitial);

        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeEnsureAwait(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkBindAsync_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var rMaybe = RUnitFactory.NoneTask(errorsInitial);

        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeEnsureAwait(() => false,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public async Task RMaybeCollectErrorsOkBindAsync_Ok_CheckNoError()
    {
        var rMaybe = RUnitFactory.SomeTask();

        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeConcatAwait(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Success);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public async Task RMaybeCollectErrorsOkBindAsync_Ok_CheckHasError()
    {
        var rMaybe = RUnitFactory.SomeTask();

        var errorBad = CreateErrorListTwoTestTask();
        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeConcatAwait(() => false,
                                                                               () => errorBad);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errorBad.Result.Count, resultAfterWhere.GetErrors().Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task RMaybeCollectErrorsOkBindAsync_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var rMaybe = RUnitFactory.NoneTask(errorInitial);

        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeConcatAwait(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task RMaybeCollectErrorsOkBindAsync_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var rMaybe = RUnitFactory.NoneTask(errorsInitial);
        var errors = CreateErrorListTwoTest();

        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeConcatAwait(() => false,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errors.Count + 1, resultAfterWhere.GetErrors().Count);
    }
}