using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.ResultExtension.ResultValueTest
{
    /// <summary>
    /// Обработка асинхронных условий для результирующего ответа задачи-объекта со связыванием с значением с возвращением к коллекции. Тесты
    /// </summary>
    public class ResultValueBindWhereToCollectionBindAsyncExtensionsTest
    {
        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе со связыванием без ошибки
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkToCollectionBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var resultValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await resultValue.ResultValueBindOkToCollectionBindAsync(
                number => RListFactory.GetRListAsync(NumberToCollection(number)));

            Assert.True(resultAfterWhere.OkStatus);
            Assert.True(NumberToCollection(initialValue).SequenceEqual(resultAfterWhere.Value));
        }

        /// <summary>
        /// Возвращение асинхронной предыдущей ошибки в результирующем ответе со связыванием с ошибкой
        /// </summary>   
        [Fact]
        public async Task ResultValueBindOkToCollectionBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var resultValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await resultValue.ResultValueBindOkToCollectionBindAsync(
                number => RListFactory.GetRListAsync(NumberToCollection(number)));

            Assert.True(resultAfterWhere.HasErrors);
            Assert.True(errorInitial.Equals(resultAfterWhere.Errors.Last()));
        }
    }
}