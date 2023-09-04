using Cinema.Domain.Common.Exceptions;

namespace Cinema.Domain.Showtime.Exceptions;

public sealed class NonPositiveMovieIdException : NonPositiveIntEntityIdException
{
    public NonPositiveMovieIdException(string message) : base(message)
    {
    }

    public static new void ThrowIfNonPositive(int id)
    {
        NonPositiveIntEntityIdException.ThrowIfNonPositive(id);
    }
}
