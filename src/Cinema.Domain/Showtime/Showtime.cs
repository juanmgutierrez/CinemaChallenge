using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Common.Exceptions;
using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.Exceptions;
using Cinema.Domain.Showtime.ValueObjects;

namespace Cinema.Domain.Showtime;

public sealed class Showtime : AggregateRoot<ShowtimeId>
{
    private Movie _movie = default!;
    private Auditorium.Auditorium _auditorium = default!;

    private Showtime(ShowtimeId id) : base(id)
    {
    }

    public DateTimeOffset SessionDate { get; private set; } = default!;

    public MovieId MovieId { get; private set; } = default!;
    public Movie Movie => _movie;

    // TODO Improve namespaces
    public AuditoriumId AuditoriumId { get; private set; } = default!;
    public Auditorium.Auditorium Auditorium => _auditorium;

    public HashSet<Ticket> Tickets { get; private set; } = new HashSet<Ticket>();

    public static Showtime Create(ShowtimeId id, DateTimeOffset sessionDate, Movie movie, Auditorium.Auditorium auditorium)
    {
        if (sessionDate <= DateTimeOffset.UtcNow)
            throw new PastDateTimeException("Session date cannot be in the past");

        return new(id)
        {
            SessionDate = sessionDate,
            MovieId = movie.Id,
            _movie = movie,
            _auditorium = auditorium
        };
    }

    public void AddTicket(Ticket ticket)
    {
        if (ticket.Showtime != this)
            throw new InvalidTicketException("Ticket does not belong to this showtime");

        Tickets.Add(ticket);
    }
}
