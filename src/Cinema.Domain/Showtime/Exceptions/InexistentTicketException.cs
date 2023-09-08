namespace Cinema.Domain.Showtime.Exceptions;

public class InexistentTicketException : Exception
{
    public InexistentTicketException(string? message) : base(message)
    {
    }
}