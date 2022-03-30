namespace ResultFunctional.Models.Enums
{
    /// <summary>
    /// Rest api error types
    /// </summary>
    public enum RestErrorType
    {
        ServerNotFound,
        BadGateway,
        BadRequest,
        GatewayTimeout,
        InternalServerError,
        RequestTimeout,
        RequestEntityToLarge,
        ValueNotFound,
        Unauthorized,
        UnsupportedMediaType,
        UnknownRestStatus,
    }
}