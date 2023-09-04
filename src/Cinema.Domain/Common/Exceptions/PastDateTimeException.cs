namespace Cinema.Domain.Common.Exceptions;

public sealed class PastDateTimeException : Exception
{
    public PastDateTimeException(string message) : base(message)
    {
    }
}
