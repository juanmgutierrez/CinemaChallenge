namespace Cinema.Domain.Auditorium.Exceptions;

public sealed class EmptySeatIdException : Exception
{
    public EmptySeatIdException(string message) : base(message)
    {
    }
}
