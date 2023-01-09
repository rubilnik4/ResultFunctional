using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Factories;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctionalXUnit.Data;
using ResultFunctionalXUnit.Mocks.Implementation;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Преобразование результирующего ответа в значение. Тесты
    /// </summary>
    public class ResultValueToValueBindAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение условия в положительном результирующем ответе со связыванием
        /// </summary>
        [Fact]
        public async Task ResultValueToValueOkBadBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValueTask = ResultValueFactory.CreateTaskResultValue(initialValue);

            var resultAfterWhere = await resultValueTask.ResultValueToValueOkBadBindAsync(
                okFunc: AsyncFunctions.IntToStringAsync,
                badFunc: errors => AsyncFunctions.IntToStringAsync(errors.Count));

            Assert.Equal(initialValue.ToString(), resultAfterWhere);
        }

        /// <summary>
        /// Возвращение предыдущей ошибки в положительном результирующем ответе с ошибкой со связыванием
        /// </summary>
        [Fact]
        public async Task ResultValueToValueOkBadBindAsync_Bad_ReturnNewValue()
        {
            var errorsInitial = ErrorData.CreateErrorListTwoTest();
            var resultValueTask = ResultValueFactory.CreateTaskResultValueError<int>(errorsInitial);

            var resultAfterWhere = await resultValueTask.ResultValueToValueOkBadBindAsync(
                okFunc: AsyncFunctions.IntToStringAsync,
                badFunc: errors => AsyncFunctions.IntToStringAsync(errors.Count));

            Assert.Equal(errorsInitial.Count.ToString(), resultAfterWhere);
        }
    }
}