using System.Linq;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async.RExtensions.Values;
using ResultFunctional.Models.Factories;
using ResultFunctionalXUnit.Data;
using Xunit;
using static ResultFunctionalXUnit.Data.ErrorData;
using static ResultFunctionalXUnit.Mocks.Implementation.SyncFunctions;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async.RExtensions.RValueTest
{
    /// <summary>
    /// Обработка асинхронных условий для результирующего ответа задачи-объекта со связыванием с значением с возвращением к коллекции. Тесты
    /// </summary>
    public class RValueToListBindAwaitExtensionsTest
    {
        /// <summary>
        /// Выполнение асинхронного положительного условия в результирующем ответе со связыванием без ошибки
        /// </summary>   
        [Fact]
        public async Task RValueBindOkToCollectionBindAsync_Ok_ReturnNewValue()
        {
            int initialValue = Numbers.Number;
            var rValue = RValueFactory.SomeTask(initialValue);

            var resultAfterWhere = await rValue.RValueToListBindSomeAwait(
                number => RListFactory.SomeTask(NumberToCollection(number)));

            Assert.True(resultAfterWhere.Success);
            Assert.True(NumberToCollection(initialValue).SequenceEqual(resultAfterWhere.GetValue()));
        }

        /// <summary>
        /// Возвращение асинхронной предыдущей ошибки в результирующем ответе со связыванием с ошибкой
        /// </summary>   
        [Fact]
        public async Task RValueBindOkToCollectionBindAsync_Bad_ReturnInitial()
        {
            var errorInitial = CreateErrorTest();
            var rValue = RValueFactory.NoneTask<int>(errorInitial);

            var resultAfterWhere = await rValue.RValueToListBindSomeAwait(
                number => RListFactory.SomeTask(NumberToCollection(number)));

            Assert.True(resultAfterWhere.Failure);
            Assert.True(errorInitial.Equals(resultAfterWhere.GetErrors().Last()));
        }
    }
}