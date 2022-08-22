using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest;

public class ResultErrorWhereAsyncExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkAsync_Ok_CheckNoError()
    {
        var resultError = new ResultError();

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkAsync(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.OkStatus);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkAsync_Ok_CheckHasError()
    {
        var resultError = new ResultError();

        var errorBad = CreateErrorListTwoTestTask();
        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkAsync(() => false,
                                                                               () => errorBad);

        Assert.True(resultAfterWhere.HasErrors);
        Assert.Equal(errorBad.Result.Count, resultAfterWhere.Errors.Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkAsync_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var resultError = new ResultError(errorInitial);

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkAsync(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.HasErrors);
        Assert.Single(resultAfterWhere.Errors);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkAsync_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var resultError = new ResultError(errorsInitial);

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkAsync(() => false,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.HasErrors);
        Assert.Single(resultAfterWhere.Errors);
    }
}