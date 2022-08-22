using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.Models.Implementations.ResultFactory;
using ResultFunctional.Models.Implementations.Results;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultErrorTest;

public class ResultErrorWhereTaskAsyncExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkTaskAsync_Ok_CheckNoError()
    {
        var resultError = ResultErrorFactory.CreateTaskResultError();

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkTaskAsync(() => true,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.OkStatus);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkTaskAsync_Ok_CheckHasError()
    {
        var resultError = ResultErrorFactory.CreateTaskResultError();

        var errorBad = CreateErrorListTwoTest();
        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkTaskAsync(() => false,
                                                                               () => errorBad);

        Assert.True(resultAfterWhere.HasErrors);
        Assert.Equal(errorBad.Count, resultAfterWhere.Errors.Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkTaskAsync_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var resultError = ResultErrorFactory.CreateTaskResultError(errorInitial);

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkTaskAsync(() => true,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.HasErrors);
        Assert.Single(resultAfterWhere.Errors);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public async Task ResultErrorCheckErrorsOkTaskAsync_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var resultError = ResultErrorFactory.CreateTaskResultError(errorsInitial);

        var resultAfterWhere = await resultError.ResultErrorCheckErrorsOkTaskAsync(() => false,
                                                                               CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.HasErrors);
        Assert.Single(resultAfterWhere.Errors);
    }
}