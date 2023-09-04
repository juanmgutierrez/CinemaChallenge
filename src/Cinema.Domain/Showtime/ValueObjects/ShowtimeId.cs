using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.Exceptions;

namespace Cinema.Domain.Showtime.ValueObjects;

public sealed class ShowtimeId : EntityId<int>
{
    public ShowtimeId(int value) : base(value)
    {
        NonPositiveShowtimeIdException.ThrowIfNonPositive(value);
    }
}
