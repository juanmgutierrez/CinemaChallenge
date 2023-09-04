namespace Cinema.Domain.Showtime.Exceptions;

public sealed class AlreadyReservedSeatException : Exception
{
    public AlreadyReservedSeatException() : base("Seat is already reserved for this show")
    {
    }
}