using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync.ResultExtension.ResultErrorTest;

public class ResultErrorWhereASyncExtensionsTest
{
    /// <summary>
    /// Выполнение условия в положительном результирующем ответе
    /// </summary>
    [Fact]
    public void ResultErrorCheckErrorsOk_Ok_CheckNoError()
    {
        var resultError = new ResultError();

        var resultAfterWhere = resultError.ResultErrorCheckErrorsOk(() => true,
                                                                    CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.OkStatus);
    }

    /// <summary>
    /// Выполнение условия в отрицательном результирующем ответе без ошибки
    /// </summary>
    [Fact]
    public void ResultErrorCheckErrorsOk_Ok_CheckHasError()
    {
        var resultError = new ResultError();

        var errorBad = CreateErrorListTwoTest();
        var resultAfterWhere = resultError.ResultErrorCheckErrorsOk(() => false,
                                                                    () => errorBad);

        Assert.True(resultAfterWhere.HasErrors);
        Assert.Equal(errorBad.Count, resultAfterWhere.Errors.Count);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public void ResultErrorCheckErrorsOk_Bad_CheckNoError()
    {
        var errorInitial = CreateErrorTest();
        var resultError = new ResultError(errorInitial);

        var resultAfterWhere = resultError.ResultErrorCheckErrorsOk(() => true,
                                                                    CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.HasErrors);
        Assert.Single(resultAfterWhere.Errors);
    }

    /// <summary>
    /// Возвращение предыдущей ошибки в отрицательном результирующем ответе с ошибкой
    /// </summary>
    [Fact]
    public void ResultErrorCheckErrorsOk_Bad_CheckHasError()
    {
        var errorsInitial = CreateErrorTest();
        var resultError = new ResultError(errorsInitial);

        var resultAfterWhere = resultError.ResultErrorCheckErrorsOk(() => false,
                                                                    CreateErrorListTwoTest);

        Assert.True(resultAfterWhere.HasErrors);
        Assert.Single(resultAfterWhere.Errors);
    }
}