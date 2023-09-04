using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.ValueObjects;

namespace Cinema.Domain.Showtime;

public sealed class Showtime : AggregateRoot<ShowtimeId>
{
    private Showtime(ShowtimeId id) : base(id)
    {
    }

    public DateTimeOffset SessionDate { get; private set; } = default!;
    public Movie Movie { get; private set; } = default!;
    public Auditorium.Auditorium Auditorium { get; private set; } = default!;
    public List<Ticket> Tickets { get; private set; } = new List<Ticket>();


}
