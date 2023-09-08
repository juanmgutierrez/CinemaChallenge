namespace Cinema.Domain.Showtime.Exceptions;

public class ExpiredTicketException : Exception
{
    public ExpiredTicketException(string message) : base(message)
    {
    }
}