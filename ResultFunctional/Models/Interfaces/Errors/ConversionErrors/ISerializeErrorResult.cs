using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Errors.ConversionErrors
{
    /// <summary>
    /// Serialize error
    /// </summary>
    public interface ISerializeErrorResult: IErrorBaseResult<ConversionErrorType>
    { }
}