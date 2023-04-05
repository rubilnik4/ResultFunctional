using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.BaseErrors;

namespace ResultFunctional.Models.Errors.ConversionErrors
{
    /// <summary>
    /// Deserialize error
    /// </summary>
    public interface IRDeserializeError : IRBaseError<ConversionErrorType>
    { }
}