using Cinema.Domain.Common.Exceptions;
using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.Exceptions;
using Cinema.Domain.Showtime.ValueObjects;

namespace Cinema.Domain.Showtime;

public sealed class Showtime : AggregateRoot<ShowtimeId>
{
    private Showtime(ShowtimeId id) : base(id)
    {
    }

    public DateTimeOffset SessionDate { get; private set; } = default!;
    public Movie Movie { get; init; } = default!;
    public Auditorium.Auditorium Auditorium { get; init; } = default!;

    public HashSet<Ticket> Tickets { get; private set; } = new HashSet<Ticket>();

    public static Showtime Create(DateTimeOffset sessionDate, Movie movie, Auditorium.Auditorium auditorium)
    {
        if (sessionDate <= DateTimeOffset.UtcNow)
            throw new PastDateTimeException("Session date cannot be in the past");

        return new(new ShowtimeId(Guid.NewGuid()))
        {
            SessionDate = sessionDate,
            Movie = movie,
            Auditorium = auditorium
        };
    }

    public void AddTicket(Ticket ticket)
    {
        if (ticket.Showtime != this)
            throw new InvalidTicketException("Ticket does not belong to this showtime");

        Tickets.Add(ticket);
    }
}
