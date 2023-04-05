using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.CommonErrors
{
    /// <summary>
    /// Not found error
    /// </summary>
    public interface IRValueNotFoundError : IRBaseError<CommonErrorType>
    { }
}