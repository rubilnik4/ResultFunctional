using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Maybe;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RMaybeTest;

public class RMaybeWhereTaskAsyncExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkTaskAsync_Ok_CheckNoError()
    {
        var RMaybe = RUnitFactory.SomeTask();

        var resultAfterWhere = await RMaybe.ToRMaybeTask().RUnitEnsureTask(() => true,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Success);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public async Task RMaybeCheckErrorsOkTaskAsync_Ok_CheckHasError()
    {
        var RMaybe = RUnitFactory.SomeTask();

        var errorBad = CreateErrorListTwoTest();
        var resultAfterWhere = await RMaybe.ToRMaybeTask().RUnitEnsureTask(() => false,
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
        var RMaybe = RUnitFactory.NoneTask(errorInitial);

        var resultAfterWhere = await RMaybe.ToRMaybeTask().RUnitEnsureTask(() => true,
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
        var RMaybe = RUnitFactory.NoneTask(errorsInitial);

        var resultAfterWhere = await RMaybe.ToRMaybeTask().RUnitEnsureTask(() => false,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }
}