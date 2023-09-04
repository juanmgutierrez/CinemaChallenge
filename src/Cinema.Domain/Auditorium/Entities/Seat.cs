using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Auditorium.Entities;

public sealed class Seat : Entity<SeatId>
{
    private Seat(SeatId id) : base(id)
    {
    }

    public int AuditoriumId { get; init; }
    public required short Row { get; init; }
    public required short SeatNumber { get; init; }

    public static Seat Create(
        SeatId id,
        int auditoriumId,
        short row,
        short seatNumber)
    {
        return new(id)
        {
            AuditoriumId = auditoriumId,
            Row = row,
            SeatNumber = seatNumber
        };
    }
}
