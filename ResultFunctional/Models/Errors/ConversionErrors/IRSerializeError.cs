using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Errors.Base;

namespace ResultFunctional.Models.Errors.ConversionErrors
{
    /// <summary>
    /// Serialize error
    /// </summary>
    public interface IRSerializeError : IRBaseError<ConversionErrorType>
    { }
}