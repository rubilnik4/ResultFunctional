using System.Collections.Generic;
using System.Linq;
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
        public static Task<IReadOnlyCollection<TValue>> ToTaskCollection<TValue>(IEnumerable<TValue> collection) =>
            Task.FromResult((IReadOnlyCollection<TValue>)collection.ToList());
    }
}