using Cinema.Domain.Auditorium.Exceptions;
using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Auditorium.Entities;

public sealed class Seat : Entity<SeatId>
{
    private Seat(SeatId id) : base(id)
    {
    }

    public required short Row { get; init; }
    public required short SeatNumber { get; init; }

    public AuditoriumId AuditoriumId { get; init; } = default!;
    public Auditorium Auditorium { get; init; } = default!;

    public static Seat Create(SeatId seatId, Auditorium auditorium, short row, short seatNumber)
    {
        if (row <= 0 || row > auditorium.Rows)
            throw new InvalidAuditoriumRowException(row);

        if (seatNumber <= 0 || seatNumber > auditorium.SeatsPerRow)
            throw new InvalidAuditoriumSeatNumberException(seatNumber);

        return new(seatId)
        {
            Row = row,
            SeatNumber = seatNumber,
            AuditoriumId = auditorium.Id,
            Auditorium = auditorium
        };
    }
}
