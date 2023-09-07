namespace Cinema.Domain.Showtime.Exceptions;

public sealed class EmptyTicketIdException : Exception
{
    public EmptyTicketIdException(string message) : base(message)
    {
    }
}
