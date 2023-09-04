namespace Cinema.Domain.Auditorium.Exceptions;

public sealed class InvalidAuditoriumSeatsPerRowNumberException : Exception
{
    public InvalidAuditoriumSeatsPerRowNumberException(int seatsPerRowNumber)
        : base($"Invalid auditorium seats per row number: {seatsPerRowNumber}")
    {
    }
}
