using System.Net;

namespace ResultFunctional.Models.Enums
{
    /// <summary>
    /// Rest api error types
    /// </summary>
    public enum RestErrorType
    {
        ServerNotFound = 1,
        BadGateway = HttpStatusCode.BadGateway,
        BadRequest = HttpStatusCode.BadRequest,
        Forbidden = HttpStatusCode.Forbidden,
        GatewayTimeout = HttpStatusCode.GatewayTimeout,
        InternalServerError = HttpStatusCode.InternalServerError,
        RequestTimeout = HttpStatusCode.RequestTimeout,
        RequestEntityToLarge = HttpStatusCode.RequestEntityTooLarge,
        ValueNotFound = HttpStatusCode.NotFound,
        Unauthorized = HttpStatusCode.Unauthorized,
        UnsupportedMediaType = HttpStatusCode.UnsupportedMediaType,
        UnknownRestStatus = -1,
    }
}