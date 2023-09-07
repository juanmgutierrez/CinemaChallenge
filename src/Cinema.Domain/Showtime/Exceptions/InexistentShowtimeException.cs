namespace Cinema.Domain.Showtime.Exceptions;

public class InexistentShowtimeException : Exception
{
    public InexistentShowtimeException(string message) : base(message)
    {
    }
}
