namespace Cinema.Domain.Showtime.Exceptions;

public sealed class EmptyShowtimeIdException : Exception
{
    public EmptyShowtimeIdException(string message) : base(message)
    {
    }
}
