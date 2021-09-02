using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для проверки условий для задачи-объекта. Тесты
    /// </summary>
    public class WhereTaskAsyncExtensionsTest
    {
        /// <summary>
        /// Условие продолжающее действие. Положительное
        /// </summary>  
        [Fact]
        public async Task WhereContinueTaskAsync_Ok()
        {
            const string test = "WhereTest";

            string testAfterWhere =
               await Task.FromResult(test).WhereContinueTaskAsync(testWhere => !String.IsNullOrWhiteSpace(testWhere),
                okFunc: testWhere => testWhere.ToLowerInvariant(),
                badFunc: testWhere => testWhere);

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }

        /// <summary>
        /// Условие продолжающее действие. Положительное. Возвращает другой тип
        /// </summary>  
        [Fact]
        public async Task WhereContinueTaskAsync_Ok_AnotherType()
        {
            const string testParseNumber = "44";

            int numberAfterTest =
               await Task.FromResult(testParseNumber).WhereContinueTaskAsync(
                 numberToParse => Int32.TryParse(numberToParse, out _),
                okFunc: Int32.Parse,
                badFunc: numberToParse => 0);

            Assert.Equal(44, numberAfterTest);
        }

        /// <summary>
        /// Условие продолжающее действие. Негативное
        /// </summary>  
        [Fact]
        public async Task WhereContinueTaskAsync_Bad()
        {
            const string test = "BadTest";

            string testAfterWhere =
                await Task.FromResult(test).WhereContinueTaskAsync(testWhere => testWhere.Length == 0,
                okFunc: testWhere => testWhere,
                badFunc: testWhere => testWhere.ToLower());

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }

        /// <summary>
        /// Условие продолжающее действие. Положительное. Возвращает другой тип
        /// </summary>  
        [Fact]
        public async Task WhereContinueTaskAsync_Bad_AnotherType()
        {
            const string testParseNumber = "test";

            int numberAfterTest =
                await Task.FromResult(testParseNumber).WhereContinueTaskAsync(
                numberToParse => Int32.TryParse(numberToParse, out _),
                okFunc: numberToParse => 0,
                badFunc: numberToParse => numberToParse.Length);

            Assert.Equal(testParseNumber.Length, numberAfterTest);
        }


        /// <summary>
        /// Обработка позитивного условия
        /// </summary>  
        [Fact]
        public async Task WhereOkTaskAsync_LowCaseText()
        {
            const string test = "WhereOk";

            string testAfterWhere =
                await Task.FromResult(test).WhereOkTaskAsync(testWhere => !String.IsNullOrWhiteSpace(testWhere),
                okFunc: testWhere => testWhere.ToLowerInvariant());

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }

        /// <summary>
        /// Обработка негативного условия
        /// </summary>
        [Fact]
        public  async Task WhereBadTaskAsync_LowCaseText()
        {
            const string test = "BadTest";

            string testAfterWhere =
                await Task.FromResult(test).WhereBadTaskAsync(testWhere => testWhere.Length == 0,
                    badFunc: testWhere => testWhere.ToLower());

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }
    }
}