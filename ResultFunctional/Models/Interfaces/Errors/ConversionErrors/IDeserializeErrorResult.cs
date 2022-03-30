using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.Base;

namespace ResultFunctional.Models.Interfaces.Errors.conversionErrors
{
    /// <summary>
    /// Deserialize error
    /// </summary>
    public interface IDeserializeErrorResult : IErrorBaseResult<ConversionErrorType>
    { }
}