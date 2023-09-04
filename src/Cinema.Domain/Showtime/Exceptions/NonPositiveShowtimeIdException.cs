using Cinema.Domain.Common.Exceptions;

namespace Cinema.Domain.Showtime.Exceptions;

public sealed class NonPositiveShowtimeIdException : NonPositiveIntEntityIdException
{
    public NonPositiveShowtimeIdException(string message) : base(message)
    {
    }

    public static new void ThrowIfNonPositive(int id)
    {
        NonPositiveIntEntityIdException.ThrowIfNonPositive(id);
    }
}
