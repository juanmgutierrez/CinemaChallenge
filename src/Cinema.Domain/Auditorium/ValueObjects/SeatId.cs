using Cinema.Domain.Auditorium.Exceptions;
using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Auditorium.ValueObjects;

public sealed class SeatId : EntityId<int>
{
    public SeatId(int value) : base(value)
    {
        NonPositiveSeatIdException.ThrowIfNonPositive(value);
    }
}
