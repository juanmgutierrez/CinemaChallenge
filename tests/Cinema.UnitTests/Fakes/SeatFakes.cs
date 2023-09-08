using Cinema.Domain.Auditorium.Entities;
using Cinema.Domain.Auditorium.ValueObjects;

namespace Cinema.UnitTests.Fakes;

internal static class SeatFakes
{
    internal static List<Seat> ValidSeatsList(short row, short fromSeatNumber, short toSeatNumber)
    {
        List<Seat> seats = new();
        for (short i = fromSeatNumber; i <= toSeatNumber; i++)
            seats.Add(Seat.Create(new SeatId(Guid.NewGuid()), AuditoriumFakes.ValidAuditorium, row, i));
        return seats;
    }
}
