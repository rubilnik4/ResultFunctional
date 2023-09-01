using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RMaybeTest;

public class RMaybeOptionTaskExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkTaskAsync_Ok_CheckNoError()
    {
        var rMaybe = RUnitFactory.SomeTask();

        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeEnsureTask(() => true,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Success);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkTaskAsync_Ok_CheckHasError()
    {
        var rMaybe = RUnitFactory.SomeTask();

        var errorBad = CreateErrorListTwoTest();
        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeEnsureTask(() => false,
                                                                               () => errorBad);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkTaskAsync_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var rMaybe = RUnitFactory.NoneTask(errorInitial);

        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeEnsureTask(() => true,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkTaskAsync_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var rMaybe = RUnitFactory.NoneTask(errorsInitial);

        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeEnsureTask(() => false,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public async Task RMaybeCollectErrorsOkTaskAsync_Ok_CheckNoError()
    {
        var rMaybe = RUnitFactory.SomeTask();

        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeConcatTask(() => true,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Success);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public async Task RMaybeCollectErrorsOkTaskAsync_Ok_CheckHasError()
    {
        var rMaybe = RUnitFactory.SomeTask();

        var errorBad = CreateErrorListTwoTest();
        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeConcatTask(() => false,
                                                                               () => errorBad);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task RMaybeCollectErrorsOkTaskAsync_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var rMaybe = RUnitFactory.NoneTask(errorInitial);

        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeConcatTask(() => true,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task RMaybeCollectErrorsOkTaskAsync_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var rMaybe = RUnitFactory.NoneTask(errorsInitial);
        var errors = CreateErrorListTwoTest();

        var resultAfterWhere = await rMaybe.ToRMaybeTask().RMaybeConcatTask(() => false,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errors.Count + 1, resultAfterWhere.GetErrors().Count);
    }
}