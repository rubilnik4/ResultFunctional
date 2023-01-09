using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Implementations.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest;

public class ResultErrorWhereBindAsyncExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkBindAsync_Ok_CheckNoError()
    {
        var resultError = ResultErrorFactory.CreateTaskResultError();

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkBindAsync(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.OkStatus);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkBindAsync_Ok_CheckHasError()
    {
        var resultError = ResultErrorFactory.CreateTaskResultError();

        var errorBad = CreateErrorListTwoTestTask();
        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkBindAsync(() => false,
                                                                               () => errorBad);

        Assert.True(resultAfterWhere.HasErrors);
        Assert.Equal(errorBad.Result.Count, resultAfterWhere.Errors.Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkBindAsync_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var resultError = ResultErrorFactory.CreateTaskResultError(errorInitial);

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkBindAsync(() => true,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.HasErrors);
        Assert.Single(resultAfterWhere.Errors);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkBindAsync_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var resultError = ResultErrorFactory.CreateTaskResultError(errorsInitial);

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkBindAsync(() => false,
                                                                               CreateErrorListTwoTestTask);

        Assert.True(resultAfterWhere.HasErrors);
        Assert.Single(resultAfterWhere.Errors);
    }
}