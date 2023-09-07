using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.Exceptions;

namespace Cinema.Domain.Showtime.ValueObjects;

public sealed class MovieImdbId : EntityId<string>
{
    public MovieImdbId(string value) : base(value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyMovieImdbIdException("ImdbId must not be null or empty");
    }
}
