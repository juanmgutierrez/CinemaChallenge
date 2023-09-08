using Cinema.Domain.Auditorium.Entities;
using Cinema.Domain.Showtime;
using Cinema.Domain.Showtime.Entities;

namespace Cinema.UnitTests.Fakes;

internal static class TicketFakes
{
    internal static Ticket ValidTicket(Showtime showtime, List<Seat> seats) =>
        Ticket.Create(
            showtime,
            seats);
}
