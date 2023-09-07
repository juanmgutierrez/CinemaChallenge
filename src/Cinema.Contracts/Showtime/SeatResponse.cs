using Cinema.Domain.Auditorium.Entities;

namespace Cinema.Contracts.Showtime;

public sealed record SeatResponse(short Row, short SeatNumber)
{
    public static SeatResponse CreateFromDomain(Seat seat) => new(seat.Row, seat.SeatNumber);
}
