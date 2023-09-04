namespace Cinema.Domain.Auditorium.Exceptions;

public sealed class InvalidAuditoriumRowException : Exception
{
    public InvalidAuditoriumRowException(int rowNumber) : base($"Invalid auditorium rows number: {rowNumber}")
    {
    }
}
