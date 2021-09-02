using System.Threading.Tasks;

namespace ResultFunctionalXUnit.Data
{
    /// <summary>
    /// Тестовые числа
    /// </summary>
    public static class Numbers
    {
        /// <summary>
        /// Тестовое число
        /// </summary>
        public static int Number =>
            2;

        /// <summary>
        /// Тестовое число
        /// </summary>
        public static Task<int?> NumberTask =>
            Task.FromResult((int?)Number);

        /// <summary>
        /// Тестовое число
        /// </summary>
        public static Task<int?> NumberTaskNull =>
            Task.FromResult<int?>(null);
    }
}