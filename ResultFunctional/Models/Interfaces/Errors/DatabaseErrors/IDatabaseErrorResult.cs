using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Errors.DatabaseErrors
{
    /// <summary>
    /// Database error
    /// </summary>
    public interface IDatabaseErrorResult : IErrorBaseResult<DatabaseErrorType>
    { }
}