using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.Exceptions;

namespace Cinema.Domain.Showtime.ValueObjects;

public sealed class MovieId : EntityId<int>
{
    public MovieId(int value) : base(value)
    {
        NonPositiveMovieIdException.ThrowIfNonPositive(value);
    }
}
