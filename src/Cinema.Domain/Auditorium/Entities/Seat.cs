using Cinema.Domain.Auditorium.Exceptions;
using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Auditorium.Entities;

public sealed class Seat : Entity<SeatId>
{
    public Seat(SeatId id, Auditorium auditorium, short row, short seatNumber) : base(id)
    {
        if (row <= 0 || row > auditorium.Rows)
            throw new InvalidAuditoriumRowException(row);

        if (seatNumber <= 0 || seatNumber > auditorium.SeatsPerRow)
            throw new InvalidAuditoriumSeatNumberException(seatNumber);

        Auditorium = auditorium;
        Row = row;
        SeatNumber = seatNumber;
    }

    public required Auditorium Auditorium { get; init; }
    public required short Row { get; init; }
    public required short SeatNumber { get; init; }
}
