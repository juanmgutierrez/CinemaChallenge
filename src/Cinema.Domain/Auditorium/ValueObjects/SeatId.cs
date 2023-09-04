using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Auditorium.ValueObjects;

public sealed class SeatId : EntityId<int>
{
    public SeatId(int value) : base(value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Id cannot be less than or equal to zero");
    }
}
