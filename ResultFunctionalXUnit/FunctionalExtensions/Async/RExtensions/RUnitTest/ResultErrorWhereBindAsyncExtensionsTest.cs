using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Units;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RUnitTest;

public class ResultErrorWhereTaskAsyncExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkTaskAsync_Ok_CheckNoError()
    {
        var resultError = RUnitFactory.SomeTask();

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkTaskAsync(() => true,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Success);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkTaskAsync_Ok_CheckHasError()
    {
        var resultError = RUnitFactory.SomeTask();

        var errorBad = CreateErrorListTwoTest();
        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkTaskAsync(() => false,
                                                                               () => errorBad);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errorBad.Count, resultAfterWhere.GetErrors().Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkTaskAsync_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var resultError = RUnitFactory.NoneTask(errorInitial);

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkTaskAsync(() => true,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkTaskAsync_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var resultError = RUnitFactory.NoneTask(errorsInitial);

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkTaskAsync(() => false,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }
}