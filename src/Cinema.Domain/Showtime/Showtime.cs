using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Common.Exceptions;
using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.Exceptions;
using Cinema.Domain.Showtime.ValueObjects;

namespace Cinema.Domain.Showtime;

public sealed class Showtime : AggregateRoot<ShowtimeId>
{
    private readonly Movie _movie;
    private readonly Auditorium.Auditorium _auditorium;

    private Showtime(ShowtimeId id, Movie movie, Auditorium.Auditorium auditorium) : base(id)
    {
        _movie = movie;
        _auditorium = auditorium;
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

        return new(id, movie, auditorium)
        {
            SessionDate = sessionDate,
            MovieId = movie.Id
        };
    }

    public void AddTicket(Ticket ticket)
    {
        if (ticket.Showtime != this)
            throw new InvalidTicketException("Ticket does not belong to this showtime");

        Tickets.Add(ticket);
    }
}
