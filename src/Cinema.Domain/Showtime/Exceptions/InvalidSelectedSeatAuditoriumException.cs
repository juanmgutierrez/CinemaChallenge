namespace Cinema.Domain.Showtime.Exceptions;

public sealed class InvalidSelectedSeatAuditoriumException : Exception
{
    public InvalidSelectedSeatAuditoriumException() : base("Selected seat does not belong to this auditorium")
    {
    }
}