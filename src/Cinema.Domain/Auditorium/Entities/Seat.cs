using Cinema.Domain.Auditorium.Exceptions;
using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Auditorium.Entities;

public sealed class Seat : Entity<SeatId>
{
    private Auditorium _auditorium = default!;

    private Seat(SeatId id) : base(id)
    {
    }

    public required short Row { get; init; }
    public required short SeatNumber { get; init; }
    
    public required AuditoriumId AuditoriumId { get; init; }
    public Auditorium Auditorium => _auditorium;

    public static Seat Create(SeatId seatId, Auditorium auditorium, short row, short seatNumber)
    {
        if (row <= 0 || row > auditorium.Rows)
            throw new InvalidAuditoriumRowException(row);

        if (seatNumber <= 0 || seatNumber > auditorium.SeatsPerRow)
            throw new InvalidAuditoriumSeatNumberException(seatNumber);

        return new(seatId)
        {
            AuditoriumId = auditorium.Id,
            Row = row,
            SeatNumber = seatNumber,
            _auditorium = auditorium
        };
    }
}
