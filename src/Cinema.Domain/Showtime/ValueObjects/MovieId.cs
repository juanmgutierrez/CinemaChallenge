using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Showtime.ValueObjects;

public sealed class MovieId : EntityId<int>
{
    public MovieId(int value) : base(value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Id cannot be less than or equal to zero");
    }
}
