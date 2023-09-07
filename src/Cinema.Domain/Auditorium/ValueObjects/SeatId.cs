using Cinema.Domain.Auditorium.Exceptions;
using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Auditorium.ValueObjects;

public sealed class SeatId : EntityId<Guid>
{
    public SeatId(Guid value) : base(value)
    {
        if (value == Guid.Empty)
            throw new EmptySeatIdException($"Seat id cannot be empty");
    }
}
