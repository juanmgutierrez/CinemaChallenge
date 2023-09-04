using Cinema.Domain.Auditorium.Exceptions;
using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Auditorium.Entities;

public sealed class Seat : Entity<SeatId>
{
    private readonly Auditorium _auditorium;

    public Seat(SeatId id, Auditorium auditorium, short row, short seatNumber) : base(id)
    {
        if (row <= 0 || row > auditorium.Rows)
            throw new InvalidAuditoriumRowException(row);

        if (seatNumber <= 0 || seatNumber > auditorium.SeatsPerRow)
            throw new InvalidAuditoriumSeatNumberException(seatNumber);

        _auditorium = auditorium;
        AuditoriumId = auditorium.Id;
        Row = row;
        SeatNumber = seatNumber;
    }

    public required short Row { get; init; }
    public required short SeatNumber { get; init; }
    
    public required AuditoriumId AuditoriumId { get; init; }
    public Auditorium Auditorium => _auditorium;
}
