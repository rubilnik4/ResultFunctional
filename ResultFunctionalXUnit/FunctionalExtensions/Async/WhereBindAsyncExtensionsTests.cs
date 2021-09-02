using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для проверки условий для асинхронной задачи-объекта. Тесты
    /// </summary>
    public class WhereBindAsyncExtensionsTests
    {
        /// <summary>
        /// Условие продолжающее действие. Положительное
        /// </summary>  
        [Fact]
        public async Task WhereContinueBindAsync_Ok()
        {
            const string test = "WhereTest";

            string testAfterWhere =
               await Task.FromResult(test).WhereContinueBindAsync(testWhere => !String.IsNullOrWhiteSpace(testWhere),
                okFunc: testWhere => Task.FromResult(testWhere.ToLowerInvariant()),
                badFunc: Task.FromResult);

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }

        /// <summary>
        /// Условие продолжающее действие. Положительное. Возвращает другой тип
        /// </summary>  
        [Fact]
        public async Task WhereContinueBindAsync_Ok_AnotherType()
        {
            const string testParseNumber = "44";

            int numberAfterTest =
               await Task.FromResult(testParseNumber).WhereContinueBindAsync(
                 numberToParse => Int32.TryParse(numberToParse, out _),
                okFunc: numberToParse => Task.FromResult(Int32.Parse(numberToParse)),
                badFunc: numberToParse => Task.FromResult(0));

            Assert.Equal(44, numberAfterTest);
        }

        /// <summary>
        /// Условие продолжающее действие. Негативное
        /// </summary>  
        [Fact]
        public async Task WhereContinueBindAsync_Bad()
        {
            const string test = "BadTest";

            string testAfterWhere =
                await Task.FromResult(test).WhereContinueBindAsync(testWhere => testWhere.Length == 0,
                okFunc: Task.FromResult,
                badFunc: testWhere => Task.FromResult(testWhere.ToLower()));

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }

        /// <summary>
        /// Условие продолжающее действие. Положительное. Возвращает другой тип
        /// </summary>  
        [Fact]
        public async Task WhereContinueBindAsync_Bad_AnotherType()
        {
            const string testParseNumber = "test";

            int numberAfterTest =
                await Task.FromResult(testParseNumber).WhereContinueBindAsync(
                numberToParse => Int32.TryParse(numberToParse, out _),
                okFunc: numberToParse => Task.FromResult(0),
                badFunc: numberToParse => Task.FromResult(numberToParse.Length));

            Assert.Equal(testParseNumber.Length, numberAfterTest);
        }


        /// <summary>
        /// Обработка позитивного условия
        /// </summary>  
        [Fact]
        public async Task WhereOkBindAsync_LowCaseText()
        {
            const string test = "WhereOk";

            string testAfterWhere =
                await Task.FromResult(test).WhereOkBindAsync(testWhere => !String.IsNullOrWhiteSpace(testWhere),
                okFunc: testWhere => Task.FromResult(testWhere.ToLowerInvariant()));

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }

        /// <summary>
        /// Обработка негативного условия
        /// </summary>
        [Fact]
        public async Task WhereBadBindAsync_LowCaseText()
        {
            const string test = "BadTest";

            string testAfterWhere =
                await Task.FromResult(test).WhereBadBindAsync(testWhere => testWhere.Length == 0,
                    badFunc: testWhere => Task.FromResult(testWhere.ToLower()));

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }
    }
}