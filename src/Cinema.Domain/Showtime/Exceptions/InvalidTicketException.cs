namespace Cinema.Domain.Showtime.Exceptions;

public class InvalidTicketException : Exception
{
    public InvalidTicketException(string message) : base(message)
    {
    }
}
