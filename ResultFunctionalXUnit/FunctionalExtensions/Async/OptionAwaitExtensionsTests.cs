using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для проверки условий для асинхронной задачи-объекта. Тесты
    /// </summary>
    public class OptionAwaitExtensionsTests
    {
        /// <summary>
        /// Условие продолжающее действие. Положительное
        /// </summary>  
        [Fact]
        public async Task WhereContinueBindAsync_Ok()
        {
            const string test = "WhereTest";

            string testAfterWhere =
               await Task.FromResult(test).OptionAwait(testWhere => !String.IsNullOrWhiteSpace(testWhere),
                testWhere => Task.FromResult(testWhere.ToLowerInvariant()),
                Task.FromResult);

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
               await Task.FromResult(testParseNumber).OptionAwait(
                 numberToParse => Int32.TryParse(numberToParse, out _),
                numberToParse => Task.FromResult(Int32.Parse(numberToParse)),
                _ => Task.FromResult(0));

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
                await Task.FromResult(test).OptionAwait(testWhere => testWhere.Length == 0,
                Task.FromResult,
                testWhere => Task.FromResult(testWhere.ToLower()));

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
                await Task.FromResult(testParseNumber).OptionAwait(
                numberToParse => Int32.TryParse(numberToParse, out _),
                _ => Task.FromResult(0),
                numberToParse => Task.FromResult(numberToParse.Length));

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
                await OptionAwaitExtensions.OptionSomeAwait(Task.FromResult(test), testWhere => !String.IsNullOrWhiteSpace(testWhere),
                                                            testWhere => Task.FromResult(testWhere.ToLowerInvariant()));

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
                await Task.FromResult(test).OptionNoneAwait(testWhere => testWhere.Length == 0,
                    testWhere => Task.FromResult(testWhere.ToLower()));

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }
    }
}