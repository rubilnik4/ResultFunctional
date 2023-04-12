using System;
using ResultFunctional.FunctionalExtensions.Sync;
using Xunit;

namespace ResultFunctionalXUnit.FunctionalExtensions.Sync
{
    /// <summary>
    /// Методы расширения для проверки условий. Тесты
    /// </summary>
    public class WhereExtensionsTest
    {
        /// <summary>
        /// Условие продолжающее действие. Положительное
        /// </summary>  
        [Fact]
        public void WhereContinue_Ok()
        {
            const string test = "WhereTest";

            string testAfterWhere = 
                test.Option(testWhere => !String.IsNullOrWhiteSpace(testWhere),
                someFunc: testWhere => testWhere.ToLowerInvariant(),
                noneFunc: testWhere => testWhere);

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }

        /// <summary>
        /// Условие продолжающее действие. Положительное. Возвращает другой тип
        /// </summary>  
        [Fact]
        public void WhereContinue_Ok_AnotherType()
        {
            const string testParseNumber = "44";

            int numberAfterTest =
                testParseNumber.Option(numberToParse => Int32.TryParse(numberToParse, out _),
                someFunc: Int32.Parse,
                noneFunc: _ => 0);

            Assert.Equal(44, numberAfterTest);
        }

        /// <summary>
        /// Условие продолжающее действие. Негативное
        /// </summary>  
        [Fact]
        public void WhereContinue_Bad()
        {
            const string test = "BadTest";

            string testAfterWhere =
                test.Option(testWhere => testWhere.Length == 0,
                someFunc: testWhere => testWhere,
                noneFunc: testWhere => testWhere.ToLower());

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }

        /// <summary>
        /// Условие продолжающее действие. Положительное. Возвращает другой тип
        /// </summary>  
        [Fact]
        public void WhereContinue_Bad_AnotherType()
        {
            const string testParseNumber = "test";

            int numberAfterTest =
                testParseNumber.Option(numberToParse => Int32.TryParse(numberToParse, out _),
                someFunc: _ => 0,
                noneFunc: numberToParse => numberToParse.Length);

            Assert.Equal(testParseNumber.Length, numberAfterTest);
        }


        /// <summary>
        /// Обработка позитивного условия
        /// </summary>  
        [Fact]
        public void WhereOk_LowCaseText()
        {
            const string test = "WhereOk";

            string testAfterWhere =
                test.OptionSome(testWhere => !String.IsNullOrWhiteSpace(testWhere),
                someFunc: testWhere => testWhere.ToLowerInvariant());

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }

        /// <summary>
        /// Обработка негативного условия
        /// </summary>
        [Fact]
        public void WhereBad_LowCaseText()
        {
            const string test = "BadTest";

            string testAfterWhere =
                test.OptionNone(testWhere => testWhere.Length == 0,
                    noneFunc: testWhere => testWhere.ToLower());

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }
    }
}