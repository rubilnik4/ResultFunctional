using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.CommonErrors
{
    /// <summary>
    /// Duplicate error
    /// </summary>
    public interface IRValueDuplicateError : IRBaseError<CommonErrorType>
    { }
}