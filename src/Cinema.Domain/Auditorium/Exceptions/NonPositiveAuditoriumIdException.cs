namespace Cinema.Domain.Auditorium.Exceptions;

public sealed class EmptyAuditoriumIdException : Exception
{
    public EmptyAuditoriumIdException(string message) : base(message)
    {
    }
}
