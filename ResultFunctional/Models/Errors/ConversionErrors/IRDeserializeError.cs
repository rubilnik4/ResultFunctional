using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Errors.ConversionErrors
{
    /// <summary>
    /// Deserialize error
    /// </summary>
    public interface IRDeserializeError : IRBaseError<ConversionErrorType>
    { }
}