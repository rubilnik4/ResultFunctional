using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Errors.DatabaseErrors
{
    /// <summary>
    /// Ошибка таблицы базы данных
    /// </summary>
    public interface IDatabaseAccessErrorResult : IDatabaseErrorResult
    { }
}