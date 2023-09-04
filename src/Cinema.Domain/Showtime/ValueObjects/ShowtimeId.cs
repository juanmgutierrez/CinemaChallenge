using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Showtime.ValueObjects;

public sealed class ShowtimeId : EntityId<int>
{
    public ShowtimeId(int value) : base(value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Id cannot be less than or equal to zero");
    }
}
