using Cinema.Application.Showtime.Commands.PayTicket;
using Cinema.Domain.Auditorium.Entities;
using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.Exceptions;
using Cinema.Domain.Showtime.ValueObjects;

namespace Cinema.Domain.Showtime.Entities;

public class Ticket : Entity<TicketId>
{
    private const int UnpaidTicketExpirationMinutes = 10;

    private Ticket(TicketId id) : base(id)
    {
        CreatedAt = DateTimeOffset.Now;
    }

    public DateTimeOffset CreatedAt { get; init; }
    public bool Paid { get; private set; } = false;

    public ShowtimeId ShowtimeId { get; init; } = default!;
    public Showtime Showtime { get; init; } = default!;

    public required List<Seat> Seats { get; init; }

    public static Ticket Create(Showtime showtime, List<Seat> selectedSeats)
    {
        if (selectedSeats.Any(selectedSeat => selectedSeat.Auditorium != showtime.Auditorium))
            throw new InvalidSelectedSeatAuditoriumException();

        HashSet<Seat> selectedSeatsHashSet = new(selectedSeats.Count);

        foreach (Seat seat in selectedSeats)
            if (!selectedSeatsHashSet.Add(seat))
                throw new DuplicateSeatsException();

        if (showtime.Tickets
            .Where(ticket => ticket.IsActiveTicket())
            .Any(ticket => ticket.Seats
                .Any(seat => selectedSeatsHashSet.Contains(seat))))
            throw new AlreadyReservedSeatException();

        return new Ticket(
            new TicketId(Guid.NewGuid()))
        {
            Seats = selectedSeats,
            ShowtimeId = showtime.Id,
            Showtime = showtime
        };
    }

    public void Pay()
    {
        if (IsExpired())
            throw new ExpiredTicketException("Ticket is expired");

        if (Paid)
            throw new AlreadyPaidTicketException("Ticket is already paid");

        Paid = true;
    }

    private bool IsActiveTicket() => Paid || CreatedAt.AddMinutes(UnpaidTicketExpirationMinutes) > DateTimeOffset.Now;
    private bool IsExpired() => CreatedAt.AddMinutes(UnpaidTicketExpirationMinutes) <= DateTimeOffset.Now;
}
