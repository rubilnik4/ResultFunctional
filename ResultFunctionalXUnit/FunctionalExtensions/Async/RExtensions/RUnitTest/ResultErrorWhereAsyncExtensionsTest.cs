using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtension.Units;
using ResultFunctional.Models.Factories;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RUnitTest;

public class ResultErrorWhereAsyncExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkAsync_Ok_CheckNoError()
    {
        var resultError = RUnitFactory.Some(); 

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkAsync(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Success);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkAsync_Ok_CheckHasError()
    {
        var resultError = RUnitFactory.Some();

        var errorBad = CreateErrorListTwoTestTask();
        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkAsync(() => false,
                                                                               () => errorBad);

        Assert.True(resultAfterWhere.Failure);
        Assert.Equal(errorBad.Result.Count, resultAfterWhere.GetErrors().Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkAsync_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var resultError = RUnitFactory.None(errorInitial);

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkAsync(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkAsync_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var resultError = RUnitFactory.None(errorsInitial);

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkAsync(() => false,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.Failure);
        Assert.Single(resultAfterWhere.GetErrors());
    }
}