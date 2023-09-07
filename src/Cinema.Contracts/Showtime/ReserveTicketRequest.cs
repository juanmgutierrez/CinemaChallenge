using Cinema.Application.Showtime.Commands.ReserveTicket;
using Cinema.Domain.Showtime.ValueObjects;

namespace Cinema.Contracts.Showtime;

public sealed record ReserveTicketRequest(int ShowtimeId, short Row, short FromSeatNumber, short ToSeatNumber)
{
    public ReserveTicketCommand ToCommand() => new(new ShowtimeId(ShowtimeId), Row, FromSeatNumber, ToSeatNumber);
}
