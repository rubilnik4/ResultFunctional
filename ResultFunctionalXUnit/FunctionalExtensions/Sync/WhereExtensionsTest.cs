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
                test.WhereContinue(testWhere => !String.IsNullOrWhiteSpace(testWhere),
                okFunc: testWhere => testWhere.ToLowerInvariant(),
                badFunc: testWhere => testWhere);

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
                testParseNumber.WhereContinue(numberToParse => Int32.TryParse(numberToParse, out _),
                okFunc: Int32.Parse,
                badFunc: _ => 0);

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
                test.WhereContinue(testWhere => testWhere.Length == 0,
                okFunc: testWhere => testWhere,
                badFunc: testWhere => testWhere.ToLower());

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
                testParseNumber.WhereContinue(numberToParse => Int32.TryParse(numberToParse, out _),
                okFunc: _ => 0,
                badFunc: numberToParse => numberToParse.Length);

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
                test.WhereOk(testWhere => !String.IsNullOrWhiteSpace(testWhere),
                okFunc: testWhere => testWhere.ToLowerInvariant());

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
                test.WhereBad(testWhere => testWhere.Length == 0,
                    badFunc: testWhere => testWhere.ToLower());

            Assert.Equal(test.ToLowerInvariant(), testAfterWhere);
        }
    }
}