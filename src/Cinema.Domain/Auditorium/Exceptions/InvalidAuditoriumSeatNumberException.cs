namespace Cinema.Domain.Auditorium.Exceptions;

public sealed class InvalidAuditoriumSeatNumberException : Exception
{
    public InvalidAuditoriumSeatNumberException(int seatNumber) : base($"Invalid auditorium rows number: {seatNumber}")
    {
    }
}
