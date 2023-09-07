using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.Exceptions;

namespace Cinema.Domain.Showtime.ValueObjects;

public sealed class ShowtimeId : EntityId<Guid>
{
    public ShowtimeId(Guid value) : base(value)
    {
        if (value == Guid.Empty)
            throw new EmptyShowtimeIdException($"Showtime id cannot be empty");
    }
}
