namespace Cinema.Application.Showtime.Commands.PayTicket;

public class ExpiredTicketException : Exception
{
    public ExpiredTicketException(string message) : base(message)
    {
    }
}