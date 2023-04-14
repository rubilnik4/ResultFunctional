using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Units;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RUnitTest;

public class ResultErrorWhereBindAsyncExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkBindAsync_Ok_CheckNoError()
    {
        var resultError = RUnitFactory.SomeTask();

        var resultAfterWhere = await resultError.RUnitEnsureAwait(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Success);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkBindAsync_Ok_CheckHasError()
    {
        var resultError = RUnitFactory.SomeTask();

        var errorBad = CreateErrorListTwoTestTask();
        var resultAfterWhere = await resultError.RUnitEnsureAwait(() => false,
                                                                               () => errorBad);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errorBad.Result.Count, resultAfterWhere.GetErrors().Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkBindAsync_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var resultError = RUnitFactory.NoneTask(errorInitial);

        var resultAfterWhere = await resultError.RUnitEnsureAwait(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkBindAsync_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var resultError = RUnitFactory.NoneTask(errorsInitial);

        var resultAfterWhere = await resultError.RUnitEnsureAwait(() => false,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }
}