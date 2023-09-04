namespace Cinema.Domain.Showtime.Exceptions;

public sealed class DuplicateSeatsException : Exception
{
    public DuplicateSeatsException() : base("Duplicated Seats")
    {
    }
}
