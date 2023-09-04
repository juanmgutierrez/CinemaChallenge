using Cinema.Domain.Auditorium.Entities;
using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.ValueObjects;

namespace Cinema.Domain.Showtime.Entities;

public class Ticket : Entity<TicketId>
{
    private Ticket(TicketId id) : base(id)
    {
        CreatedTime = DateTimeOffset.Now;
    }

    public DateTimeOffset CreatedTime { get; init; }
    public Showtime Showtime { get; init; } = null!;
    public List<Seat> Seats { get; init; } = new List<Seat>();
    public bool Paid { get; private set; } = false;

    public void Pay() => Paid = true;

    public static Ticket Create(Showtime showtime, List<Seat> selectedSeats) =>
        new(new TicketId(Guid.NewGuid()))
        {
            Seats = selectedSeats,
            Showtime = showtime
        };
}
