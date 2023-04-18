using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RMaybeTest;

public class RMaybeWhereBindAsyncExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkBindAsync_Ok_CheckNoError()
    {
        var RMaybe = RUnitFactory.SomeTask();

        var resultAfterWhere = await RMaybe.ToRMaybeTask().RUnitEnsureAwait(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Success);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkBindAsync_Ok_CheckHasError()
    {
        var RMaybe = RUnitFactory.SomeTask();

        var errorBad = CreateErrorListTwoTestTask();
        var resultAfterWhere = await RMaybe.ToRMaybeTask().RUnitEnsureAwait(() => false,
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
        var RMaybe = RUnitFactory.NoneTask(errorInitial);

        var resultAfterWhere = await RMaybe.ToRMaybeTask().RUnitEnsureAwait(() => true,
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
        var RMaybe = RUnitFactory.NoneTask(errorsInitial);

        var resultAfterWhere = await RMaybe.ToRMaybeTask().RUnitEnsureAwait(() => false,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }
}