using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Errors.DatabaseErrors
{
    /// <summary>
    /// Table access database error
    /// </summary>
    public interface IRDatabaseAccessError : IDatabaseErrorResult
    { }
}