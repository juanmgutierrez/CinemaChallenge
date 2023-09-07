namespace Cinema.Domain.Showtime.Exceptions;

public sealed class EmptyMovieImdbIdException : Exception
{
    public EmptyMovieImdbIdException(string message) : base(message)
    {
    }
}
