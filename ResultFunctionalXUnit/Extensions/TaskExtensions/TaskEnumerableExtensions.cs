using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResultFunctionalXUnit.Extensions.TaskExtensions
{
    /// <summary>
    /// Методы расширения для задач
    /// </summary>
    public static class TaskEnumerableExtensions
    {
        /// <summary>
        /// Преобразовать множество в задачу
        /// </summary>
        public static Task<IEnumerable<TValue>> ToTaskEnumerable<TValue>(IEnumerable<TValue> collection) =>
            Task.FromResult(collection);
    }
}