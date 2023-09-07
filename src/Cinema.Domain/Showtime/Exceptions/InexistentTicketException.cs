namespace Cinema.Application.Showtime.Commands.PayTicket;

public class InexistentTicketException : Exception
{
    public InexistentTicketException(string? message) : base(message)
    {
    }
}