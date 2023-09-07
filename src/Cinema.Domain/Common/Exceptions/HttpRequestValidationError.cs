namespace Cinema.Domain.Common.Exceptions;

// TODO Delete if not anymore needed
internal class HttpRequestValidationError
{
    public HttpRequestValidationError(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }

    public required string PropertyName { get; init; }
    public required string ErrorMessage { get; init; }
}
