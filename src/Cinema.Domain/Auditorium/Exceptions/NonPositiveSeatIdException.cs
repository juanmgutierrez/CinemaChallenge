using Cinema.Domain.Common.Exceptions;

namespace Cinema.Domain.Auditorium.Exceptions;

public sealed class NonPositiveSeatIdException : NonPositiveIntEntityIdException
{
    public NonPositiveSeatIdException(string message) : base(message)
    {
    }

    public static new void ThrowIfNonPositive(int id)
    {
        NonPositiveIntEntityIdException.ThrowIfNonPositive(id);
    }
}
