using System;
using System.Threading.Tasks;
using ResultFunctional.FunctionalExtensions.Async;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Async
{
    /// <summary>
    /// Методы расширения для проверки условий для задачи-объекта. Тесты
    /// </summary>
    public class OptionTaskExtensionsTest
    {
        /// <summary>
        /// Условие продолжающее действие. Положительное
        /// </summary>  
        [Fact]
        public async Task WhereContinueTaskAsync_Ok()
        {
            const string test = "WhereTest";

            string testAfterWhere =
               await Task.FromResult(test).OptionTask(testWhere => !String.IsNullOrWhiteSpace(testWhere),
                testWhere => testWhere.ToLowerInvariant(),
                testWhere => testWhere);

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
               await Task.FromResult(testParseNumber).OptionTask(
                 numberToParse => Int32.TryParse(numberToParse, out _),
                Int32.Parse,
                _ => 0);

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
                await Task.FromResult(test).OptionTask(testWhere => testWhere.Length == 0,
                testWhere => testWhere,
                testWhere => testWhere.ToLower());

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
                await Task.FromResult(testParseNumber).OptionTask(
                numberToParse => Int32.TryParse(numberToParse, out _),
                _ => 0,
                numberToParse => numberToParse.Length);

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
                await Task.FromResult(test).OptionSomeTask(testWhere => !String.IsNullOrWhiteSpace(testWhere),
                testWhere => testWhere.ToLowerInvariant());

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
                await Task.FromResult(test).OptionNoneTask(testWhere => testWhere.Length == 0,
                    testWhere => testWhere.ToLower());

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }
    }
}