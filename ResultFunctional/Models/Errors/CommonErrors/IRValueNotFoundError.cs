using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Errors.CommonErrors
{
    /// <summary>
    /// Not found error
    /// </summary>
    public interface IRValueNotFoundError : IRBaseError<CommonErrorType>
    { }
}