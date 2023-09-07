using Cinema.Application.Showtime.Commands.PayTicket;
using Cinema.Domain.Showtime.ValueObjects;

namespace Cinema.Contracts.Showtime;

public sealed record PayTicketRequest(Guid Id)
{
    public PayTicketCommand ToCommand() => new(new TicketId(Id));
}
