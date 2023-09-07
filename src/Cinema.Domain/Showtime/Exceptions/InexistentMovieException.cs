namespace Cinema.Domain.Showtime.Exceptions;

public class InexistentMovieException : Exception
{
    public InexistentMovieException(string message) : base(message)
    {
    }
}