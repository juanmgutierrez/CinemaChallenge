namespace Cinema.Domain.Showtime.Exceptions;

public class AlreadyPaidTicketException : Exception
{
    public AlreadyPaidTicketException(string message) : base(message)
    {
    }
}