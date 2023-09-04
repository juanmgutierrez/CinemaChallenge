using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.Exceptions;

namespace Cinema.Domain.Showtime.ValueObjects;

public sealed class MovieId : EntityId<string>
{
    public MovieId(string value) : base(value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyMovieIdException("Movie id cannot be empty or null");
    }
}
