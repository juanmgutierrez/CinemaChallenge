using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.Exceptions;

namespace Cinema.Domain.Showtime.ValueObjects;

public sealed class MovieId : EntityId<Guid>
{
    public MovieId(Guid value) : base(value)
    {
        if (value == Guid.Empty)
            throw new EmptyMovieIdException($"Movie id cannot be empty");
    }
}
