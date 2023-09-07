using Cinema.Domain.Auditorium.Entities;

namespace Cinema.Contracts.Auditorium;

public sealed record SeatResponse(Guid SeatId, short Row, short SeatNumber)
{
    public static SeatResponse CreateFromDomain(Seat seat) => new(seat.Id.Value, seat.Row, seat.SeatNumber);
}
