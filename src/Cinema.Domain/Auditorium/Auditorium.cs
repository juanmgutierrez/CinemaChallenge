using Cinema.Domain.Auditorium.Exceptions;
using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Auditorium;

public sealed class Auditorium : AggregateRoot<AuditoriumId>
{
    public Auditorium(AuditoriumId id, short rows, short seatsPerRow) : base(id)
    {
        if (rows <= 0)
            throw new InvalidAuditoriumRowsNumberException(rows);

        if (seatsPerRow <= 0)
            throw new InvalidAuditoriumSeatsPerRowNumberException(seatsPerRow);

        Rows = rows;
        SeatsPerRow = seatsPerRow;
    }

    public short Rows { get; init; }
    public short SeatsPerRow { get; init; }
}
