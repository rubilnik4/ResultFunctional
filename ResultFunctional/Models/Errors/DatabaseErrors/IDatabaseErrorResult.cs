using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Errors.DatabaseErrors
{
    /// <summary>
    /// Database error
    /// </summary>
    public interface IDatabaseErrorResult : IRBaseError<DatabaseErrorType>
    { }
}