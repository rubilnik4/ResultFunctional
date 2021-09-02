using System.Threading.Tasks;

namespace ResultFunctionalXUnit.Data
{
    public static class StringTest
    {
        /// <summary>
        /// Тестовая строка
        /// </summary>
        public static string? TestString =>
            "Test";

        /// <summary>
        /// Тестовая строка. Задача
        /// </summary>
        public static Task<string?> TestStringTask =>
            Task.FromResult(TestString);

        /// <summary>
        /// Тестовая строка. Задача
        /// </summary>
        public static Task<string?> TestStringTaskNull =>
            Task.FromResult<string?>(null);
    }
}