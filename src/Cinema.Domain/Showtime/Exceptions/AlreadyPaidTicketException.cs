namespace Cinema.Application.Showtime.Commands.PayTicket;

public class AlreadyPaidTicketException : Exception
{
    public AlreadyPaidTicketException(string message) : base(message)
    {
    }
}