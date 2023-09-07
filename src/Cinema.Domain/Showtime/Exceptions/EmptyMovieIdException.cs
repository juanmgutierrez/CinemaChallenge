namespace Cinema.Domain.Showtime.Exceptions;

public sealed class EmptyMovieIdException : Exception
{
    public EmptyMovieIdException(string message) : base(message)
    {
    }
}
