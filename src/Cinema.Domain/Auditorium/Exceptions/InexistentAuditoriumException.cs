namespace Cinema.Domain.Auditorium.Exceptions;

public class InexistentAuditoriumException : Exception
{
    public InexistentAuditoriumException(string message) : base(message)
    {
    }
}